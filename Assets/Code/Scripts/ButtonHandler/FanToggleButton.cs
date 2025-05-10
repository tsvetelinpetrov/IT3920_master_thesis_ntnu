using UnityEngine;

public class FanToggleButton : MonoBehaviour
{
    private DualToggleSwitch lightsToggleSwitch;

    void Start()
    {
        // Initialize button and its components
        lightsToggleSwitch = GetComponent<DualToggleSwitch>();

        // Initialize button state
        UpdateButtonVisuals();
    }

    void OnEnable()
    {
        // Subscribe to events
        EventCenter.Controls.OnTurnOnUpperFan += HandleFansOn;
        EventCenter.Controls.OnTurnOnLowerFan += HandleFansOn;
        EventCenter.Controls.OnTurnOffUpperFan += HandleFansOff;
        EventCenter.Controls.OnTurnOffLowerFan += HandleFansOff;
    }

    void OnDisable()
    {
        // Unsubscribe when object is destroyed
        EventCenter.Controls.OnTurnOnUpperFan -= HandleFansOn;
        EventCenter.Controls.OnTurnOnLowerFan -= HandleFansOn;
        EventCenter.Controls.OnTurnOffUpperFan -= HandleFansOff;
        EventCenter.Controls.OnTurnOffLowerFan -= HandleFansOff;
    }

    public void OnToggleOnClicked()
    {
        ToggleFan(true);
    }

    public void OnToggleOffClicked()
    {
        ToggleFan(false);
    }

    private void ToggleFan(bool newState)
    {
        // Set button to a disabled state
        lightsToggleSwitch?.Disable();

        IDataSource dataSource = DataSourceFactory.GetDataSource();

        // Disable API calls to prevent data mismatch
        GlobalSettings.Instance.BlockAPICalls = true;

        // Call API to change fan state
        dataSource.ControlFans(
            newState,
            success =>
            {
                lightsToggleSwitch?.Enable();

                // Disable API calls to prevent data mismatch
                GlobalSettings.Instance.BlockAPICalls = false;
            },
            error =>
            {
                lightsToggleSwitch?.Enable();

                // Keep visuals the same as before
                UpdateButtonVisuals();

                // Disable API calls to prevent data mismatch
                GlobalSettings.Instance.BlockAPICalls = false;

                Debug.LogError($"Failed to toggle fan: {error}");
            }
        );
    }

    private void UpdateButtonVisuals()
    {
        // Update the button visuals based on the current state
        if (GlobalSettings.Instance.UpperFanStatus || GlobalSettings.Instance.LowerFanStatus)
        {
            lightsToggleSwitch?.ChangeStateWithoutEventInvoke(false);
        }
        else
        {
            lightsToggleSwitch?.ChangeStateWithoutEventInvoke(true);
        }
    }

    private void HandleFansOn()
    {
        // Handle the event when fans are turned on
        lightsToggleSwitch?.ChangeStateWithoutEventInvoke(false);
    }

    private void HandleFansOff()
    {
        // Handle the event when fans are turned off
        lightsToggleSwitch?.ChangeStateWithoutEventInvoke(true);
    }
}
