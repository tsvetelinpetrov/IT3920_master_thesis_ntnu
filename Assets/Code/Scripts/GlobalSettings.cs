using UnityEngine;

// Struct to define the operating modes for the Digital Twin (Standalone or API)
public enum OperatingMode
{
    Standalone,
    Realtime,
}

public class GlobalSettings : MonoBehaviour
{
    public static GlobalSettings Instance { get; private set; }

    // Global settings (vars that should be accessible from any script)
    public OperatingMode OperatingMode = OperatingMode.Realtime;
    public string ApiUrl =
        "https://greenhouse-data-api-ddefcwbncabfftbv.canadacentral-01.azurewebsites.net/";
    public DataSourceType DataSourceType = DataSourceType.Api;

    // Private vals
    private bool _lightsStatus = false;
    private bool _upperFanStatus = false;
    private bool _lowerFanStatus = false;

    public bool LightsStatus
    {
        get => _lightsStatus;
    }

    public bool UpperFanStatus
    {
        get => _upperFanStatus;
    }

    public bool LowerFanStatus
    {
        get => _lowerFanStatus;
    }

    void OnEnable()
    {
        EventCenter.Controls.OnTurnOnLights += SetLightingOn;
        EventCenter.Controls.OnTurnOffLights += SetLightingOff;
        EventCenter.Controls.OnTurnOnUpperFan += SetUpperFanOn;
        EventCenter.Controls.OnTurnOffUpperFan += SetUpperFanOff;
        EventCenter.Controls.OnTurnOnLowerFan += SetLowerFanOn;
        EventCenter.Controls.OnTurnOffLowerFan += SetLowerFanOff;
    }

    void OnDisable()
    {
        EventCenter.Controls.OnTurnOnLights -= SetLightingOn;
        EventCenter.Controls.OnTurnOffLights -= SetLightingOff;
        EventCenter.Controls.OnTurnOnUpperFan -= SetUpperFanOn;
        EventCenter.Controls.OnTurnOffUpperFan -= SetUpperFanOff;
        EventCenter.Controls.OnTurnOnLowerFan -= SetLowerFanOn;
        EventCenter.Controls.OnTurnOffLowerFan -= SetLowerFanOff;
    }

    private void SetLightingOn()
    {
        _lightsStatus = true;
    }

    private void SetLightingOff()
    {
        _lightsStatus = false;
    }

    private void SetUpperFanOn()
    {
        _upperFanStatus = true;
    }

    private void SetUpperFanOff()
    {
        _upperFanStatus = false;
    }

    private void SetLowerFanOn()
    {
        _lowerFanStatus = true;
    }

    private void SetLowerFanOff()
    {
        _lowerFanStatus = false;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
