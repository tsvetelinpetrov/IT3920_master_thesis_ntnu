using UnityEngine;

public class LightToggleButton : MonoBehaviour
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
        EventCenter.Controls.OnTurnOnLights += HandleLightsOnEvent;
        EventCenter.Controls.OnTurnOffLights += HandleLightsOffEvent;
    }

    void OnDisable()
    {
        // Unsubscribe from events
        EventCenter.Controls.OnTurnOnLights -= HandleLightsOnEvent;
        EventCenter.Controls.OnTurnOffLights -= HandleLightsOffEvent;
    }

    public void OnToggleOnClicked()
    {
        ToggleLight(true);
    }

    public void OnToggleOffClicked()
    {
        ToggleLight(false);
    }

    private void ToggleLight(bool newState)
    {
        // Set button to a disabled state
        lightsToggleSwitch?.Disable();

        IDataSource dataSource = DataSourceFactory.GetDataSource();

        // Disable API calls to prevent data mismatch
        GlobalParameters.Instance.BlockAPICalls = true;

        // Call API to change light state
        dataSource.ControlLight(
            newState,
            success =>
            {
                lightsToggleSwitch?.Enable();
                GlobalParameters.Instance.BlockAPICalls = false;
            },
            error =>
            {
                lightsToggleSwitch?.Enable();

                // Keep visuals the same as before
                UpdateButtonVisuals();

                // Re-enable API calls
                GlobalParameters.Instance.BlockAPICalls = false;

                Debug.LogError($"Failed to toggle light: {error}");
            }
        );
    }

    private void UpdateButtonVisuals()
    {
        if (GlobalParameters.Instance.LightsStatus)
            lightsToggleSwitch?.ChangeStateWithoutEventInvoke(false);
        else
            lightsToggleSwitch?.ChangeStateWithoutEventInvoke(true);
    }

    private void HandleLightsOnEvent()
    {
        // Handle the event when lights are turned on
        lightsToggleSwitch?.ChangeStateWithoutEventInvoke(false);
    }

    private void HandleLightsOffEvent()
    {
        // Handle the event when lights are turned off
        lightsToggleSwitch?.ChangeStateWithoutEventInvoke(true);
    }
}
