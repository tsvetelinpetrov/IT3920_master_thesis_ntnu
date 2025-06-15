using UnityEngine;

// Struct to define the operating modes for the Digital Twin (Standalone or API)
public enum OperatingMode
{
    Standalone,
    Realtime,
}

public enum PlantQuality
{
    Low,
    High,
}

public enum PlantModelObtainment
{
    ObtainFromDataSource,
    LoadDummyModel,
    DoNotLoadAnything,
}

public class GlobalParameters : MonoBehaviour
{
    public static GlobalParameters Instance { get; private set; }

    [Header("Global Settings")]
    // Global settings (vars that should be accessible from any script)
    [SerializeField]
    [Tooltip("The URL of the API to be used for data retrieval")]
    public string ApiUrl = "http://10.53.8.177:8000/";

    [SerializeField]
    [Tooltip("The type of data source to be used (API or File)")]
    public DataSourceType DataSourceType = DataSourceType.Api;

    [SerializeField]
    [Tooltip("Fetch current data from the API every x seconds")]
    public int CurrentDataRefreshRate = 3; // In seconds

    [SerializeField]
    [Tooltip("The method to obtain the plant model in the scene")]
    public PlantModelObtainment PlantModelObtainment = PlantModelObtainment.ObtainFromDataSource;

    [SerializeField]
    [Tooltip("The quality of the plant model to be retrieved from the API and used in the scene")]
    public PlantQuality PlantModelQuality = PlantQuality.Low;

    // Private vals
    private bool _lightsStatus = true;
    private bool _upperFanStatus = false;
    private bool _lowerFanStatus = false;
    private bool _valveStatus = false;
    private float _heaterDutyCycle = 0.0f;
    private bool _blockAPICalls = false;

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

    public bool ValveStatus
    {
        get => _valveStatus;
    }

    public float HeaterDutyCycle
    {
        get => _heaterDutyCycle;
        set => _heaterDutyCycle = value;
    }

    public bool BlockAPICalls
    {
        get => _blockAPICalls;
        set => _blockAPICalls = value;
    }

    void OnEnable()
    {
        EventCenter.Controls.OnTurnOnLights += SetLightingOn;
        EventCenter.Controls.OnTurnOffLights += SetLightingOff;
        EventCenter.Controls.OnTurnOnUpperFan += SetUpperFanOn;
        EventCenter.Controls.OnTurnOffUpperFan += SetUpperFanOff;
        EventCenter.Controls.OnTurnOnLowerFan += SetLowerFanOn;
        EventCenter.Controls.OnTurnOffLowerFan += SetLowerFanOff;
        EventCenter.Controls.OnValveOpen += SetValveOpen;
        EventCenter.Controls.OnValveClose += SetValveClose;
    }

    void OnDisable()
    {
        EventCenter.Controls.OnTurnOnLights -= SetLightingOn;
        EventCenter.Controls.OnTurnOffLights -= SetLightingOff;
        EventCenter.Controls.OnTurnOnUpperFan -= SetUpperFanOn;
        EventCenter.Controls.OnTurnOffUpperFan -= SetUpperFanOff;
        EventCenter.Controls.OnTurnOnLowerFan -= SetLowerFanOn;
        EventCenter.Controls.OnTurnOffLowerFan -= SetLowerFanOff;
        EventCenter.Controls.OnValveOpen -= SetValveOpen;
        EventCenter.Controls.OnValveClose -= SetValveClose;
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

    private void SetValveOpen()
    {
        _valveStatus = true;
    }

    private void SetValveClose()
    {
        _valveStatus = false;
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
