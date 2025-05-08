using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

[System.Serializable]
public class PlantData
{
    public string name { get; set; }
    public string description { get; set; }
    public string best_light_condition { get; set; }
    public string best_soil_type { get; set; }
    public string best_watering { get; set; }
}
