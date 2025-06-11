using UnityEngine;

public class ValveToggleButton : MonoBehaviour
{
    private DualToggleSwitch valveToggleSwitch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize button and its components
        valveToggleSwitch = GetComponent<DualToggleSwitch>();

        // Initialize button state
        UpdateButtonVisuals();
    }

    void OnEnable()
    {
        // Subscribe to events
        EventCenter.Controls.OnValveOpen += HandleValveOpen;
        EventCenter.Controls.OnValveClose += HandleValveClose;
    }

    void OnDisable()
    {
        // Unsubscribe when object is destroyed
        EventCenter.Controls.OnValveOpen -= HandleValveOpen;
        EventCenter.Controls.OnValveClose -= HandleValveClose;
    }

    public void OnToggleOnClicked()
    {
        ToggleValve(true);
    }

    public void OnToggleOffClicked()
    {
        ToggleValve(false);
    }

    private void ToggleValve(bool newState)
    {
        // Set button to a disabled state
        valveToggleSwitch?.Disable();

        IDataSource dataSource = DataSourceFactory.GetDataSource();

        // Disable API calls to prevent data mismatch
        GlobalSettings.Instance.BlockAPICalls = true;

        // Call API to change valve state
        dataSource.ControlWaterValve(
            newState,
            success =>
            {
                valveToggleSwitch?.Enable();

                // Re-enable API calls
                GlobalSettings.Instance.BlockAPICalls = false;
            },
            error =>
            {
                valveToggleSwitch?.Enable();

                // Keep visuals the same as before
                UpdateButtonVisuals();

                GlobalSettings.Instance.BlockAPICalls = false;
                Debug.LogError($"Error toggling valve: {error}");
            }
        );
    }

    private void UpdateButtonVisuals()
    {
        if (GlobalSettings.Instance.ValveStatus)
        {
            valveToggleSwitch?.ChangeStateWithoutEventInvoke(false);
        }
        else
        {
            valveToggleSwitch?.ChangeStateWithoutEventInvoke(true);
        }
    }

    private void HandleValveOpen()
    {
        valveToggleSwitch?.ChangeStateWithoutEventInvoke(false);
    }

    private void HandleValveClose()
    {
        valveToggleSwitch?.ChangeStateWithoutEventInvoke(true);
    }
}
