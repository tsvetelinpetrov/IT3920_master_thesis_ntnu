using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

[System.Serializable]
public class PlantData
{
    public List<PlantClassifications> classifications { get; set; }
    public List<PlantDisease> diseases { get; set; }
}

[System.Serializable]
public class PlantClassifications
{
    public string id { get; set; }
    public string name { get; set; }
    public float probability { get; set; }
    public PlantDetails details { get; set; }
}

[System.Serializable]
public class PlantDetails
{
    [JsonProperty("common_names")]
    public List<string> commonNames { get; set; }
    public PlantDescription description { get; set; }

    [JsonProperty("best_light_condition")]
    public string bestLightCondition { get; set; }

    [JsonProperty("best_soil_type")]
    public string bestSoilType { get; set; }

    [JsonProperty("best_watering")]
    public string bestWatering { get; set; }

    public string language { get; set; }

    [JsonProperty("entity_id")]
    public string entityId { get; set; }
}

[System.Serializable]
public class PlantDescription
{
    public string value { get; set; }
    public string citation { get; set; }
}
