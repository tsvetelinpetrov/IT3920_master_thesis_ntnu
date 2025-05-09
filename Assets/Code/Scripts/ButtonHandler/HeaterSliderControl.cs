using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeaterSliderControl : MonoBehaviour
{
    [SerializeField]
    private Slider heaterSlider;

    [SerializeField]
    private Image sliderFillImage;

    [SerializeField]
    private TextMeshProUGUI valueText;

    [SerializeField]
    private Button confirmButton; // New: Confirmation button

    [SerializeField]
    private Color minColor = new Color(0.3f, 0.3f, 0.35f); // Dark gray for OFF/MIN

    [SerializeField]
    private Color maxColor = new Color(1f, 0.5f, 0f); // Orange/red for MAX

    [SerializeField]
    private Color pendingChangeColor = new Color(0.3f, 0.7f, 1f); // Blue for pending changes

    private float currentDutyCycle = 0f; // Current active duty cycle
    private float pendingDutyCycle = 0f; // Pending duty cycle (not yet sent to API)
    private bool isProcessing = false; // Is API request in progress
    private bool hasPendingChanges = false; // Are there unsaved changes

    void Start()
    {
        if (heaterSlider == null)
            heaterSlider = GetComponent<Slider>();

        // Initialize slider events
        heaterSlider.onValueChanged.AddListener(OnSliderValueChanged);

        // Initialize button event
        if (confirmButton != null)
            confirmButton.onClick.AddListener(OnConfirmButtonClicked);

        // Subscribe to control events if needed
        EventCenter.Controls.OnControlsChanged += OnControlsChanged;

        // Initialize state
        pendingDutyCycle = currentDutyCycle;
        UpdateButtonState();

        // Initialize visuals
        UpdateVisuals(currentDutyCycle, false);
    }

    void OnDestroy()
    {
        // Unsubscribe when object is destroyed
        EventCenter.Controls.OnControlsChanged -= OnControlsChanged;

        // Clean up listeners
        if (heaterSlider != null)
            heaterSlider.onValueChanged.RemoveListener(OnSliderValueChanged);

        if (confirmButton != null)
            confirmButton.onClick.RemoveListener(OnConfirmButtonClicked);
    }

    private void OnControlsChanged(Controls controls)
    {
        if (Mathf.Abs(currentDutyCycle - controls.HeaterDutyCycle) > 0.01f)
        {
            currentDutyCycle = controls.HeaterDutyCycle;
            pendingDutyCycle = currentDutyCycle;
            hasPendingChanges = false;

            // Update UI without triggering events
            heaterSlider.SetValueWithoutNotify(currentDutyCycle);
            UpdateVisuals(currentDutyCycle, false);
            UpdateButtonState();
        }
    }

    private void OnSliderValueChanged(float value)
    {
        pendingDutyCycle = value;
        hasPendingChanges = Mathf.Abs(pendingDutyCycle - currentDutyCycle) > 0.01f;

        // Update visuals to show pending state
        UpdateVisuals(value, hasPendingChanges);
        UpdateButtonState();
    }

    private void OnConfirmButtonClicked()
    {
        if (hasPendingChanges && !isProcessing)
        {
            UpdateHeaterDutyCycle(pendingDutyCycle);
        }
    }

    private void UpdateButtonState()
    {
        // Enable/disable confirm button based on pending changes and processing state
        if (confirmButton != null)
        {
            confirmButton.interactable = hasPendingChanges && !isProcessing;
        }
    }

    private void UpdateHeaterDutyCycle(float dutyCycle)
    {
        if (isProcessing)
            return;

        isProcessing = true;

        // Disable UI controls while API call is in progress
        heaterSlider.interactable = false;
        if (confirmButton != null)
            confirmButton.interactable = false;

        IDataSource dataSource = DataSourceFactory.GetDataSource();
        dataSource.ControlHeater(
            dutyCycle,
            success =>
            {
                // Update state after successful API call
                currentDutyCycle = dutyCycle;
                pendingDutyCycle = dutyCycle;
                hasPendingChanges = false;
                isProcessing = false;

                // Re-enable UI
                heaterSlider.interactable = true;
                UpdateButtonState();

                // Update visuals to reflect current state
                UpdateVisuals(currentDutyCycle, false);

                Debug.Log($"Heater duty cycle set to {dutyCycle:F2} successfully");
            },
            error =>
            {
                // API call failed, revert to previous state
                isProcessing = false;
                heaterSlider.interactable = true;

                // TODO: Maybe show an error message to the user and revert the slider to the last confirmed value
                // Keep the pending change in the slider
                UpdateButtonState();

                Debug.LogError($"Failed to set heater duty cycle: {error}");
            }
        );
    }

    private void UpdateVisuals(float value, bool isPending)
    {
        // Update fill color
        if (sliderFillImage != null)
        {
            if (isPending)
            {
                // Use pending color for unsaved changes
                sliderFillImage.color = pendingChangeColor;
            }
            else
            {
                // Use normal color gradient for confirmed value
                sliderFillImage.color = Color.Lerp(minColor, maxColor, value);
            }
        }

        // Update text
        if (valueText != null)
        {
            // Format as percentage
            int percent = Mathf.RoundToInt(value * 100);
            string status = isPending ? " (Pending)" : "";
            valueText.text = $"Heater: {percent}%{status}";
        }
    }

    public void SetHeaterDutyCycle(float dutyCycle)
    {
        // External method to set the duty cycle (e.g., from code)
        dutyCycle = Mathf.Clamp01(dutyCycle);

        currentDutyCycle = dutyCycle;
        pendingDutyCycle = dutyCycle;
        hasPendingChanges = false;

        heaterSlider.SetValueWithoutNotify(dutyCycle);
        UpdateVisuals(dutyCycle, false);
        UpdateButtonState();
    }
}
