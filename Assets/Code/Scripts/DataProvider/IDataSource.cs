using System;
using System.Collections.Generic;

/// <summary>
/// Interface for data sources which provide identical methods for obtaining data from the API or file.
/// </summary>
/// <remarks>
/// The data source type is set in the global settings and can be either API (ApiSource) or file (FileSource).
/// </remarks>
public interface IDataSource
{
    /// <summary>
    /// Get plant model from the API
    /// </summary>
    /// <param name="successCallback">Callback function to handle the response</param>
    /// <param name="highPoly">Whether to get the high or low poly model</param>
    /// <param name="errorCallback">Callback function to handle the error response</param>
    /// <remarks>
    /// The successCallback function should take a string parameter.
    /// </remarks>
    void GetPlantObjModel(
        System.Action<string> successCallback,
        bool highPoly = false,
        System.Action<string> errorCallback = null
    );

    /// <summary>
    /// Get current data from the API
    /// </summary>
    /// <param name="successCallback">Callback function to handle the response</param>
    /// <param name="errorCallback">Callback function to handle the error response</param>
    /// <remarks>
    /// The successCallback function should take a Current parameter.
    /// </remarks>
    void GetAllCurrent(
        System.Action<Current> successCallback,
        System.Action<string> errorCallback = null
    );

    /// <summary>
    /// Get controls by days from the API
    /// </summary>
    /// <param name="days">Number of days to get controls for</param>
    /// <param name="successCallback">Callback function to handle the response</param>
    /// <param name="errorCallback">Callback function to handle the error response</param>
    /// <remarks>
    /// The successCallback function should take a List of Controls parameter.
    /// </remarks>
    void GetControlsByDays(
        int days,
        System.Action<List<Controls>> successCallback,
        System.Action<string> errorCallback = null
    );

    /// <summary>
    /// Get controls by interval from the API
    /// </summary>
    /// <param name="startTime">Start time of the interval</param>
    /// <param name="endTime">End time of the interval</param>
    /// <param name="successCallback">Callback function to handle the response</param>
    /// <param name="errorCallback">Callback function to handle the error response</param>
    /// <remarks>
    /// The successCallback function should take a List of Controls parameter.
    /// </remarks>
    void GetControlsByInterval(
        DateTime startTime,
        DateTime endTime,
        System.Action<List<Controls>> successCallback,
        System.Action<string> errorCallback = null
    );

    /// <summary>
    /// Get current controls from the API
    /// </summary>
    /// <param name="successCallback">Callback function to handle the response</param>
    /// <param name="errorCallback">Callback function to handle the error response</param>
    /// <remarks>
    /// The successCallback function should take a Controls parameter.
    /// </remarks>
    void GetCurrentControls(
        System.Action<Controls> successCallback,
        System.Action<string> errorCallback = null
    );

    /// <summary>
    /// Get measurements by days from the API
    /// </summary>
    /// <param name="days">Number of days to get measurements for</param>
    /// <param name="successCallback">Callback function to handle the response</param>
    /// <param name="errorCallback">Callback function to handle the error response</param>
    /// <remarks>
    /// The successCallback function should take a List of Measurement parameter.
    /// </remarks>
    void GetMeasurementsByDays(
        int days,
        System.Action<List<Measurement>> successCallback,
        System.Action<string> errorCallback = null
    );

    /// <summary>
    /// Get measurements by interval from the API
    /// </summary>
    /// <param name="startTime">Start time of the interval</param>
    /// <param name="endTime">End time of the interval</param>
    /// <param name="successCallback">Callback function to handle the response</param>
    /// <param name="errorCallback">Callback function to handle the error response</param>
    /// <remarks>
    /// The successCallback function should take a List of Measurement parameter.
    /// </remarks>
    void GetMeasurementsByInterval(
        DateTime startTime,
        DateTime endTime,
        System.Action<List<Measurement>> successCallback,
        System.Action<string> errorCallback = null
    );

    /// <summary>
    /// Get current measurements from the API
    /// </summary>
    /// <param name="successCallback">Callback function to handle the response</param>
    /// <param name="errorCallback">Callback function to handle the error response</param>
    /// <remarks>
    /// The successCallback function should take a Measurement parameter.
    /// </remarks>
    void GetCurrentMeasurements(
        System.Action<Measurement> successCallback,
        System.Action<string> errorCallback = null
    );

    /// <summary>
    /// Get disruptive by days from the API
    /// </summary>
    /// <param name="days">Number of days to get disruptive for</param>
    /// <param name="successCallback">Callback function to handle the response</param>
    /// <param name="errorCallback">Callback function to handle the error response</param>
    /// <remarks>
    /// The successCallback function should take a List of Disruptive parameter.
    /// </remarks>
    void GetDisruptiveByDays(
        int days,
        System.Action<List<Disruptive>> successCallback,
        System.Action<string> errorCallback = null
    );

    /// <summary>
    /// Get disruptive by interval from the API
    /// </summary>
    /// <param name="startTime">Start time of the interval</param>
    /// <param name="endTime">End time of the interval</param>
    /// <param name="successCallback">Callback function to handle the response</param>
    /// <param name="errorCallback">Callback function to handle the error response</param>
    /// <remarks>
    /// The successCallback function should take a List of Disruptive parameter.
    /// </remarks>
    void GetDisruptiveByInterval(
        DateTime startTime,
        DateTime endTime,
        System.Action<List<Disruptive>> successCallback,
        System.Action<string> errorCallback = null
    );

    /// <summary>
    /// Get current disruptive from the API
    /// </summary>
    /// <param name="successCallback">Callback function to handle the response</param>
    /// <param name="errorCallback">Callback function to handle the error response</param>
    /// <remarks>
    /// The successCallback function should take a Disruptive parameter.
    /// </remarks>
    void GetCurrentDisruptive(
        System.Action<List<Disruptive>> successCallback,
        System.Action<string> errorCallback = null
    );

    /// <summary>
    /// Get current airflow from the API
    /// </summary>
    /// <param name="successCallback">Callback function to handle the response</param>
    /// <param name="errorCallback">Callback function to handle the error response</param>
    /// <remarks>
    /// The successCallback function should take a List of Airflow parameter.
    /// </remarks>
    void GetCurrentAirflow(
        System.Action<Airflow> successCallback,
        System.Action<string> errorCallback = null
    );
}
