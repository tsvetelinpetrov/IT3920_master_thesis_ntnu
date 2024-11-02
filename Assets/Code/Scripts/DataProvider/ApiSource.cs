using System.Collections.Generic;
using System.Diagnostics;
using CI.HttpClient;
using UnityEngine;

/// <summary>
/// Structure for storing API endpoints.
/// </summary>
/// <remarks>
/// This structure is used to store the endpoints of the API that the ApiSource class reads data from.
/// </remarks>
public struct ApiEndpoints
{
    public const string ControlsByDays = "controls/days?num_days=";
    public const string PlantLowResObjModel = "mesh/low_res";
    public const string PlantHighResObjModel = "mesh/high_res";
}

/// <summary>
/// Data source for reading data from the API.
/// </summary>
/// <remarks>
/// This class implements the IDataSource interface and provides methods for reading data from the API.
/// </remarks>
public class ApiSource : IDataSource
{
    private readonly string _apiUrl = GlobalSettings.Instance.ApiUrl;
    private HttpClient client = new HttpClient();

    public ApiSource() { }

    public void GetPlantObjModel(
        System.Action<string> successCallback,
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
                successCallback(response.ReadAsString());
            }
        );
    }

    public void GetControlsByDays(
        int days,
        System.Action<List<Controls>> successCallback,
        System.Action<string> errorCallback
    )
    {
        client.Get(
            new System.Uri(_apiUrl + ApiEndpoints.ControlsByDays + days.ToString()),
            HttpCompletionOption.AllResponseContent,
            (response) =>
            {
                client.EnsureSuccess(response, errorCallback);
                successCallback(response.ReadAsJson<List<Controls>>());
            }
        );
    }
}
