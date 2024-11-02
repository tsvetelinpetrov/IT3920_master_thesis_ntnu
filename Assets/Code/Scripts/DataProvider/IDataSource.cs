using System.Collections.Generic;

public interface IDataSource
{
    /// <summary>
    /// Get plant model from the API
    /// </summary>
    /// <param name="callback">Callback function to handle the response</param>
    /// <param name="highPoly">Whether to get the high or low poly model</param>
    /// <param name="errorCallback">Callback function to handle the error response</param>
    /// <remarks>
    /// The callback function should take a string parameter.
    /// </remarks>
    void GetPlantObjModel(
        System.Action<string> callback,
        bool highPoly = false,
        System.Action<string> errorCallback = null
    );

    /// <summary>
    /// Get controls by days from the API
    /// </summary>
    /// <param name="days">Number of days to get controls for</param>
    /// <param name="callback">Callback function to handle the response</param>
    /// <param name="errorCallback">Callback function to handle the error response</param>
    /// <remarks>
    /// The callback function should take a List of Controls parameter.
    /// </remarks>
    void GetControlsByDays(
        int days,
        System.Action<List<Controls>> callback,
        System.Action<string> errorCallback = null
    );
}
