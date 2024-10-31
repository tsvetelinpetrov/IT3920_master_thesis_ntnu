

using System.Collections.Generic;
using System.Diagnostics;
using CI.HttpClient;
using UnityEngine;

public struct ApiEndpoints
{
    public const string ControlsByDays = "controls/days?num_days=";
    public const string PlantLowResObjModel = "mesh/low_res";
    public const string PlantHighResObjModel = "mesh/high_res";
}

public class ApiSource : IDataSource
{
    private readonly string _apiUrl;
    HttpClient client = new HttpClient();

    public ApiSource(string apiUrl)
    {
        _apiUrl = apiUrl;
    }

    public void GetPlantObjModel(System.Action<string> callback, bool highPoly)
    {
        string endpoint = highPoly ? ApiEndpoints.PlantHighResObjModel : ApiEndpoints.PlantLowResObjModel;
        client.Get(new System.Uri(_apiUrl + endpoint), HttpCompletionOption.AllResponseContent, (r) =>
        {
            callback(r.ReadAsString());
        });
    }

    public void GetControlsByDays(int days, System.Action<List<Controls>> callback)
    {
        client.Get(new System.Uri(_apiUrl + ApiEndpoints.ControlsByDays + days.ToString()), HttpCompletionOption.AllResponseContent, (r) =>
        {
            callback(r.ReadAsJson<List<Controls>>());
        });
    }
}