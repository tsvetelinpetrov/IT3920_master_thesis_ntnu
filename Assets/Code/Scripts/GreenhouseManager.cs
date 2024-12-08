using System.Collections.Generic;
using UnityEngine;

public class GreenhouseManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GlobalSettings.Instance.OperatingMode == OperatingMode.Realtime)
        {
            // Each 3 seconds, get the current controls
            // TODO: Change the initialize method to get all current data (InitializeAllCurrentData)
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
                ProcessControlsData(controls);
            },
            (error) => Debug.LogError($"Failed to get control data: {error}")
        );
    }

    private void InitializeAllCurrentData()
    {
        IDataSource dataSource = DataSourceFactory.GetDataSource();

        // Call GetAllCurrent
        dataSource.GetAllCurrent(
            (current) =>
            {
                ProcessControlsData(current.Controls);
                ProcessMeasurementsData(current.Measurements);
                ProcessDisruptiveData(current.Disruptive);
            },
            (error) => Debug.LogError($"Failed to get current data: {error}")
        );
    }

    private void ProcessControlsData(Controls controls)
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
            && (GlobalSettings.Instance.UpperFanStatus || GlobalSettings.Instance.LowerFanStatus)
        )
        {
            EventCenter.Controls.TurnOffUpperFan();
            EventCenter.Controls.TurnOffLowerFan();
        }

        if (controls.ValveOpen && !GlobalSettings.Instance.ValveStatus)
        {
            EventCenter.Controls.OpenValve();
        }
        else if (!controls.ValveOpen && GlobalSettings.Instance.ValveStatus)
        {
            EventCenter.Controls.CloseValve();
        }
    }

    private void ProcessMeasurementsData(Measurement measurements)
    {
        // TODO: Implement the logic to process the measurements data
    }

    private void ProcessDisruptiveData(List<Disruptive> disruptive)
    {
        // TODO: Implement the logic to process the disruptive data
    }
}
