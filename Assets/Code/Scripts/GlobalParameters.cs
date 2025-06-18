using UnityEngine;

public class GlobalParameters : MonoBehaviour
{
    public static GlobalParameters Instance { get; private set; }

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
