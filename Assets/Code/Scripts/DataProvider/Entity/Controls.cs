using System;
using Newtonsoft.Json;

[System.Serializable]
public class Controls
{
    [JsonConverter(typeof(UnixEpochMillisecondsConverter))]
    public DateTime MeasurementTime { get; set; }
    public float HeaterDutyCycle { get; set; }
    public bool LightOn { get; set; }
    public bool FanOn { get; set; }
    public bool ValveOpen { get; set; }
}
