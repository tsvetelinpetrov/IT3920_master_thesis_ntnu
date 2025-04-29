using System;
using System.Collections.Generic;
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
    public const string Current = "current";
    public const string ControlsByDays = "controls/days?num_days=";
    public const string PlantLowResObjModel = "mesh/low_res";
    public const string PlantHighResObjModel = "mesh/high_res";
    public const string ControlsByInterval = "controls/interval?start_time={0}&end_time={1}";
    public const string CurrentControls = "controls/current";
    public const string MeasurementsByDays = "measurements/days?num_days=";
    public const string MeasurementsByInterval =
        "measurements/interval?start_time={0}&end_time={1}";
    public const string CurrentMeasurements = "measurements/current";
    public const string DisruptiveByDays = "disruptive/days?num_days=";
    public const string DisruptiveByInterval = "disruptive/interval?start_time={0}&end_time={1}";
    public const string CurrentDisruptive = "disruptive/current";
    public const string CurrentAirflow = "reconstruction";
    public const string ControlLight = "controls/light?state={0}";
    public const string ControlFans = "controls/fans?state={0}";
    public const string ControlHeater = "controls/heater?value={0}";
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

    public void GetAllCurrent(
        System.Action<Current> successCallback,
        System.Action<string> errorCallback
    )
    {
        string endpoint = ApiEndpoints.Current;

        client.Get(
            new System.Uri(_apiUrl + endpoint),
            HttpCompletionOption.AllResponseContent,
            (response) =>
            {
                client.EnsureSuccess(response, errorCallback);
                successCallback(response.ReadAsJson<Current>());
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

    public void GetControlsByInterval(
        DateTime startTime,
        DateTime endTime,
        System.Action<List<Controls>> successCallback,
        System.Action<string> errorCallback
    )
    {
        string startEpoch = new DateTimeOffset(startTime).ToUnixTimeMilliseconds().ToString();
        string endEpoch = new DateTimeOffset(endTime).ToUnixTimeMilliseconds().ToString();

        string endpoint = string.Format(ApiEndpoints.ControlsByInterval, startEpoch, endEpoch);

        client.Get(
            new System.Uri(_apiUrl + endpoint),
            HttpCompletionOption.AllResponseContent,
            (response) =>
            {
                client.EnsureSuccess(response, errorCallback);
                successCallback(response.ReadAsJson<List<Controls>>());
            }
        );
    }

    public void GetCurrentControls(
        System.Action<Controls> successCallback,
        System.Action<string> errorCallback
    )
    {
        string endpoint = ApiEndpoints.CurrentControls;

        client.Get(
            new System.Uri(_apiUrl + endpoint),
            HttpCompletionOption.AllResponseContent,
            (response) =>
            {
                client.EnsureSuccess(response, errorCallback);
                successCallback(response.ReadAsJson<Controls>());
            }
        );
    }

    public void GetMeasurementsByDays(
        int days,
        System.Action<List<Measurement>> successCallback,
        System.Action<string> errorCallback
    )
    {
        string endpoint = ApiEndpoints.MeasurementsByDays + days.ToString();

        client.Get(
            new System.Uri(_apiUrl + endpoint),
            HttpCompletionOption.AllResponseContent,
            (response) =>
            {
                client.EnsureSuccess(response, errorCallback);
                successCallback(response.ReadAsJson<List<Measurement>>());
            }
        );
    }

    public void GetMeasurementsByInterval(
        DateTime startTime,
        DateTime endTime,
        System.Action<List<Measurement>> successCallback,
        System.Action<string> errorCallback
    )
    {
        string startEpoch = new DateTimeOffset(startTime).ToUnixTimeMilliseconds().ToString();
        string endEpoch = new DateTimeOffset(endTime).ToUnixTimeMilliseconds().ToString();

        string endpoint = string.Format(ApiEndpoints.MeasurementsByInterval, startEpoch, endEpoch);

        client.Get(
            new System.Uri(_apiUrl + endpoint),
            HttpCompletionOption.AllResponseContent,
            (response) =>
            {
                client.EnsureSuccess(response, errorCallback);
                successCallback(response.ReadAsJson<List<Measurement>>());
            }
        );
    }

    public void GetCurrentMeasurements(
        System.Action<Measurement> successCallback,
        System.Action<string> errorCallback
    )
    {
        string endpoint = ApiEndpoints.CurrentMeasurements;

        client.Get(
            new System.Uri(_apiUrl + endpoint),
            HttpCompletionOption.AllResponseContent,
            (response) =>
            {
                client.EnsureSuccess(response, errorCallback);
                successCallback(response.ReadAsJson<Measurement>());
            }
        );
    }

    public void GetDisruptiveByDays(
        int days,
        System.Action<List<Disruptive>> successCallback,
        System.Action<string> errorCallback
    )
    {
        string endpoint = ApiEndpoints.DisruptiveByDays + days.ToString();

        client.Get(
            new System.Uri(_apiUrl + endpoint),
            HttpCompletionOption.AllResponseContent,
            (response) =>
            {
                client.EnsureSuccess(response, errorCallback);
                successCallback(response.ReadAsJson<List<Disruptive>>());
            }
        );
    }

    public void GetDisruptiveByInterval(
        DateTime startTime,
        DateTime endTime,
        System.Action<List<Disruptive>> successCallback,
        System.Action<string> errorCallback
    )
    {
        string startEpoch = new DateTimeOffset(startTime).ToUnixTimeMilliseconds().ToString();
        string endEpoch = new DateTimeOffset(endTime).ToUnixTimeMilliseconds().ToString();

        string endpoint = string.Format(ApiEndpoints.DisruptiveByInterval, startEpoch, endEpoch);

        client.Get(
            new System.Uri(_apiUrl + endpoint),
            HttpCompletionOption.AllResponseContent,
            (response) =>
            {
                client.EnsureSuccess(response, errorCallback);
                successCallback(response.ReadAsJson<List<Disruptive>>());
            }
        );
    }

    public void GetCurrentDisruptive(
        Action<List<Disruptive>> successCallback,
        Action<string> errorCallback = null
    )
    {
        string endpoint = ApiEndpoints.CurrentDisruptive;

        client.Get(
            new System.Uri(_apiUrl + endpoint),
            HttpCompletionOption.AllResponseContent,
            (response) =>
            {
                client.EnsureSuccess(response, errorCallback);
                successCallback(response.ReadAsJson<List<Disruptive>>());
            }
        );
    }

    public void GetCurrentAirflow(
        Action<Airflow> successCallback,
        Action<string> errorCallback = null
    )
    {
        string endpoint = ApiEndpoints.CurrentAirflow;

        client.Get(
            new System.Uri(_apiUrl + endpoint),
            HttpCompletionOption.AllResponseContent,
            (response) =>
            {
                client.EnsureSuccess(response, errorCallback);
                successCallback(response.ReadAsJson<Airflow>());
            }
        );
    }

    public void ControlLight(
        bool state,
        System.Action<bool> successCallback,
        System.Action<string> errorCallback = null
    )
    {
        string endpoint = string.Format(ApiEndpoints.ControlLight, state.ToString().ToLower());

        client.Post(
            new System.Uri(_apiUrl + endpoint),
            HttpCompletionOption.AllResponseContent,
            (response) =>
            {
                client.EnsureSuccess(response, errorCallback);
                successCallback(state);
            }
        );
    }

    public void ControlFans(
        bool state,
        System.Action<bool> successCallback,
        System.Action<string> errorCallback = null
    )
    {
        string endpoint = string.Format(ApiEndpoints.ControlFans, state.ToString().ToLower());

        client.Post(
            new System.Uri(_apiUrl + endpoint),
            HttpCompletionOption.AllResponseContent,
            (response) =>
            {
                client.EnsureSuccess(response, errorCallback);
                successCallback(state);
            }
        );
    }

    public void ControlHeater(
        float dutyCycle,
        System.Action<float> successCallback,
        System.Action<string> errorCallback = null
    )
    {
        // Clamp duty cycle value between 0 and 1
        dutyCycle = Mathf.Clamp01(dutyCycle);

        string endpoint = string.Format(
            ApiEndpoints.ControlHeater,
            dutyCycle.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)
        );

        client.Post(
            new System.Uri(_apiUrl + endpoint),
            HttpCompletionOption.AllResponseContent,
            (response) =>
            {
                client.EnsureSuccess(response, errorCallback);
                successCallback(dutyCycle);
            }
        );
    }
}
