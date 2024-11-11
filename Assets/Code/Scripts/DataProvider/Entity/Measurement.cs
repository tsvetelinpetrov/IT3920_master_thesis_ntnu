using System;
using Newtonsoft.Json;

[System.Serializable]
public class Measurement
{
    [JsonConverter(typeof(UnixEpochMillisecondsConverter))]
    public DateTime MeasurementTime { get; set; }
    public float Temperature { get; set; }
    public float Moisture { get; set; }
    public float CO2 { get; set; }
    public float Humidity { get; set; }
    public float TankLevel { get; set; }
}
