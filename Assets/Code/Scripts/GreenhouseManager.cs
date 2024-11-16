using UnityEngine;

public class GreenhouseManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GlobalSettings.Instance.OperatingMode == OperatingMode.Realtime)
        {
            // Each 10 seconds, get the current controls
            InvokeRepeating("InitializeCurrentControls", 0, 3);
        }
        else
        {
            // TODO: Implement the Standalone mode logic if needed (probably we can skip it and just use the API/File mode)
        }
    }

    // Update is called once per frame
    void Update() { }

    private void InitializeCurrentControls()
    {
        IDataSource dataSource = DataSourceFactory.GetDataSource();

        // Call GetCurrentControls
        dataSource.GetCurrentControls(
            (controls) =>
            {
                if (controls.LightOn && !GlobalSettings.Instance.LightsStatus)
                {
                    EventCenter.Controls.TurnOnLights();
                }
                else if (!controls.LightOn && GlobalSettings.Instance.LightsStatus)
                {
                    EventCenter.Controls.TurnOffLights();
                }

                if (
                    controls.FanOn
                    && !GlobalSettings.Instance.UpperFanStatus
                    && !GlobalSettings.Instance.LowerFanStatus
                )
                {
                    EventCenter.Controls.TurnOnUpperFan();
                    EventCenter.Controls.TurnOnLowerFan();
                }
                else if (
                    !controls.FanOn
                    && (
                        GlobalSettings.Instance.UpperFanStatus
                        || GlobalSettings.Instance.LowerFanStatus
                    )
                )
                {
                    EventCenter.Controls.TurnOffUpperFan();
                    EventCenter.Controls.TurnOffLowerFan();
                }
            },
            (error) => Debug.LogError($"Failed to get control data: {error}")
        );
    }
}
