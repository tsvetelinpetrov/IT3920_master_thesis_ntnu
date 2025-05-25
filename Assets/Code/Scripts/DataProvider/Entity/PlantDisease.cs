using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

[System.Serializable]
public class PlantDisease
{
    public string id { get; set; }
    public string name { get; set; }
    public float probability { get; set; }
    public PlantDiseaseDetail details { get; set; }
}

public class PlantDiseaseDetail
{
    public string description { get; set; }

    [JsonProperty("common_names")]
    public List<string> commonNames { get; set; }
    public string language { get; set; }

    [JsonProperty("entity_id")]
    public string entityId { get; set; }
}
