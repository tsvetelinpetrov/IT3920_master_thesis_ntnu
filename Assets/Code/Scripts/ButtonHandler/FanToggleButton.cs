using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FanToggleButton : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private Image buttonImage;

    [SerializeField]
    private TextMeshProUGUI buttonText;

    [SerializeField]
    private Color onColor = new Color(0.5f, 0.8f, 1f); // Cool blue for ON

    [SerializeField]
    private Color offColor = new Color(0.3f, 0.3f, 0.35f); // Dark gray for OFF

    private bool currentFanState;
    private bool isProcessing = false;

    void Start()
    {
        if (button == null)
            button = GetComponent<Button>();

        button.onClick.AddListener(ToggleFan);

        // Initialize based on global settings
        currentFanState =
            GlobalSettings.Instance.UpperFanStatus && GlobalSettings.Instance.LowerFanStatus;
        UpdateButtonVisuals();

        // Subscribe to fan status change events
        EventCenter.Controls.OnTurnOnUpperFan += HandleFansOn;
        EventCenter.Controls.OnTurnOnLowerFan += HandleFansOn;
        EventCenter.Controls.OnTurnOffUpperFan += HandleFansOff;
        EventCenter.Controls.OnTurnOffLowerFan += HandleFansOff;
    }

    void OnDestroy()
    {
        // Unsubscribe when object is destroyed
        EventCenter.Controls.OnTurnOnUpperFan -= HandleFansOn;
        EventCenter.Controls.OnTurnOnLowerFan -= HandleFansOn;
        EventCenter.Controls.OnTurnOffUpperFan -= HandleFansOff;
        EventCenter.Controls.OnTurnOffLowerFan -= HandleFansOff;
    }

    private void HandleFansOn()
    {
        // Only update if both fans are now on
        if (GlobalSettings.Instance.UpperFanStatus && GlobalSettings.Instance.LowerFanStatus)
        {
            UpdateFanState(true);
        }
    }

    private void HandleFansOff()
    {
        // Update if either fan is turned off
        if (!GlobalSettings.Instance.UpperFanStatus || !GlobalSettings.Instance.LowerFanStatus)
        {
            UpdateFanState(false);
        }
    }

    private void ToggleFan()
    {
        if (isProcessing)
            return;

        isProcessing = true;
        button.interactable = false;

        bool newState = !currentFanState;
        IDataSource dataSource = DataSourceFactory.GetDataSource();

        // Call API to change fan state
        dataSource.ControlFans(
            newState,
            success =>
            {
                currentFanState = newState;
                isProcessing = false;
                button.interactable = true;

                UpdateButtonVisuals();

                // No need to broadcast events here as the API response will trigger
                // the appropriate events through your existing ProcessControlsData method

                Debug.Log($"Fan turned {(newState ? "ON" : "OFF")} successfully");
            },
            error =>
            {
                isProcessing = false;
                button.interactable = true;

                UpdateButtonVisuals();

                Debug.LogError($"Failed to toggle fan: {error}");
            }
        );
    }

    private void UpdateButtonVisuals()
    {
        if (buttonImage != null)
            buttonImage.color = currentFanState ? onColor : offColor;

        if (buttonText != null)
            buttonText.text = currentFanState ? "Fan: ON" : "Fan: OFF";
    }

    public void UpdateFanState(bool newState)
    {
        if (currentFanState != newState)
        {
            currentFanState = newState;
            UpdateButtonVisuals();
        }
    }
}
