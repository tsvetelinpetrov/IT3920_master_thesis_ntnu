using System;

[System.Serializable]
public class Controls
{
    public Int64 MeasurementTime { get; set; }
    public float HeaterDutyCycle { get; set; }
    public bool LightOn { get; set; }
    public bool FanOn { get; set; }
    public bool ValveOpen { get; set; }
}
