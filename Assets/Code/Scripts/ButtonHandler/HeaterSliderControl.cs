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
    private Color minColor = new Color(0.3f, 0.3f, 0.35f); // Dark gray for OFF/MIN

    [SerializeField]
    private Color maxColor = new Color(1f, 0.5f, 0f); // Orange/red for MAX

    [SerializeField]
    private float updateDelay = 0.5f; // Delay in seconds before sending API request

    private float currentDutyCycle = 0f;
    private bool isProcessing = false;
    private float lastChangeTime = 0f;
    private float pendingValue = -1f;

    void Start()
    {
        if (heaterSlider == null)
            heaterSlider = GetComponent<Slider>();

        // Initialize slider events
        heaterSlider.onValueChanged.AddListener(OnSliderValueChanged);

        // Subscribe to control events if needed
        EventCenter.Controls.OnControlsChanged += OnControlsChanged;

        // Initialize visuals
        UpdateVisuals(currentDutyCycle);
    }

    void OnDestroy()
    {
        // Unsubscribe when object is destroyed
        EventCenter.Controls.OnControlsChanged -= OnControlsChanged;
    }

    void Update()
    {
        // Check if we have a pending update and enough time has passed
        if (pendingValue >= 0 && Time.time - lastChangeTime > updateDelay && !isProcessing)
        {
            UpdateHeaterDutyCycle(pendingValue);
            pendingValue = -1f;
        }
    }

    private void OnControlsChanged(Controls controls)
    {
        // Assuming Controls class has a HeaterDutyCycle field
        if (Mathf.Abs(currentDutyCycle - controls.HeaterDutyCycle) > 0.01f)
        {
            currentDutyCycle = controls.HeaterDutyCycle;
            heaterSlider.SetValueWithoutNotify(currentDutyCycle);
            UpdateVisuals(currentDutyCycle);
        }
    }

    private void OnSliderValueChanged(float value)
    {
        // Update visuals immediately for responsive UI
        UpdateVisuals(value);

        // Store the time of the change and the value
        lastChangeTime = Time.time;
        pendingValue = value;
    }

    private void UpdateHeaterDutyCycle(float dutyCycle)
    {
        if (isProcessing)
            return;

        isProcessing = true;

        // Disable slider while API call is in progress
        heaterSlider.interactable = false;

        IDataSource dataSource = DataSourceFactory.GetDataSource();
        dataSource.ControlHeater(
            dutyCycle,
            success =>
            {
                // Update state after successful API call
                currentDutyCycle = dutyCycle;
                isProcessing = false;
                heaterSlider.interactable = true;

                Debug.Log($"Heater duty cycle set to {dutyCycle:F2} successfully");
            },
            error =>
            {
                // API call failed, revert to previous state
                isProcessing = false;
                heaterSlider.interactable = true;

                // Revert slider to previous value
                heaterSlider.SetValueWithoutNotify(currentDutyCycle);
                UpdateVisuals(currentDutyCycle);

                Debug.LogError($"Failed to set heater duty cycle: {error}");
            }
        );
    }

    private void UpdateVisuals(float value)
    {
        // Update fill color - lerp between min and max colors
        if (sliderFillImage != null)
            sliderFillImage.color = Color.Lerp(minColor, maxColor, value);

        // Update text
        if (valueText != null)
        {
            // Format as percentage
            int percent = Mathf.RoundToInt(value * 100);
            valueText.text = $"Heater: {percent}%";
        }
    }

    public void SetHeaterDutyCycle(float dutyCycle)
    {
        // External method to set the duty cycle (e.g., from code)
        dutyCycle = Mathf.Clamp01(dutyCycle);
        if (Mathf.Abs(currentDutyCycle - dutyCycle) > 0.01f)
        {
            currentDutyCycle = dutyCycle;
            heaterSlider.SetValueWithoutNotify(dutyCycle);
            UpdateVisuals(dutyCycle);
        }
    }
}
