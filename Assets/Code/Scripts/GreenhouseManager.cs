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
                Debug.Log($"Received control data: {controls.LightOn}");
                if (controls.LightOn)
                {
                    EventCenter.Controls.TurnOnLights();
                }
                else
                {
                    EventCenter.Controls.TurnOffLights();
                }
            },
            (error) => Debug.LogError($"Failed to get control data: {error}")
        );
    }
}
