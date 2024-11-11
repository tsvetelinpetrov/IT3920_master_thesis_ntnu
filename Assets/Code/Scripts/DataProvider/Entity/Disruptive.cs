using System;
using Newtonsoft.Json;

[System.Serializable]
public class Disruptive
{
    [JsonConverter(typeof(UnixEpochMillisecondsConverter))]
    public DateTime MeasurementTime { get; set; }
    public string SensorID { get; set; }
    public float Temperature { get; set; }
    public float Humidity { get; set; }
    public int Rn { get; set; }
}
