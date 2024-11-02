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
    private readonly string _apiUrl = GlobalSettings.Instance.ApiUrl;
    private HttpClient client = new HttpClient();

    public ApiSource() { }

    public void GetPlantObjModel(
        System.Action<string> callback,
        bool highPoly,
        System.Action<string> errorCallback
    )
    {
        string endpoint = highPoly
            ? ApiEndpoints.PlantHighResObjModel
            : ApiEndpoints.PlantLowResObjModel;
        client.Get(
            new System.Uri(_apiUrl + endpoint),
            HttpCompletionOption.AllResponseContent,
            (response) =>
            {
                client.EnsureSuccess(response, errorCallback);
                callback(response.ReadAsString());
            }
        );
    }

    public void GetControlsByDays(
        int days,
        System.Action<List<Controls>> callback,
        System.Action<string> errorCallback
    )
    {
        client.Get(
            new System.Uri(_apiUrl + ApiEndpoints.ControlsByDays + days.ToString()),
            HttpCompletionOption.AllResponseContent,
            (response) =>
            {
                client.EnsureSuccess(response, errorCallback);
                callback(response.ReadAsJson<List<Controls>>());
            }
        );
    }
}
