using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Structure for storing file paths.
/// </summary>
/// <remarks>
/// This structure is used to store the paths of the files that the FileSource class reads data from.
/// </remarks>
public struct FilesLocations
{
    // TODO: Add the file paths
}

/// <summary>
/// Data source for reading data from files.
/// </summary>
/// <remarks>
/// This class implements the IDataSource interface and provides methods for reading data from files.
/// </remarks>
public class FileSource : IDataSource
{
    public FileSource() { }

    public void GetPlantObjModel(
        System.Action<string> successCallback,
        bool highPoly,
        System.Action<string> errorCallback
    )
    {
        // TODO: Implement this method
        throw new System.NotImplementedException();
    }

    public void GetAllCurrent(
        System.Action<Current> successCallback,
        System.Action<string> errorCallback
    )
    {
        // TODO: Implement this method
        throw new System.NotImplementedException();
    }

    public void GetControlsByDays(
        int days,
        System.Action<List<Controls>> successCallback,
        System.Action<string> errorCallback
    )
    {
        // TODO: Implement this method
        throw new System.NotImplementedException();
    }

    public void GetControlsByInterval(
        DateTime startTime,
        DateTime endTime,
        Action<List<Controls>> successCallback,
        Action<string> errorCallback = null
    )
    {
        // TODO: Implement this method
        throw new NotImplementedException();
    }

    public void GetCurrentControls(
        Action<Controls> successCallback,
        Action<string> errorCallback = null
    )
    {
        // TODO: Implement this method
        throw new NotImplementedException();
    }

    public void GetMeasurementsByDays(
        int days,
        Action<List<Measurement>> successCallback,
        Action<string> errorCallback = null
    )
    {
        // TODO: Implement this method
        throw new NotImplementedException();
    }

    public void GetMeasurementsByInterval(
        DateTime startTime,
        DateTime endTime,
        Action<List<Measurement>> successCallback,
        Action<string> errorCallback = null
    )
    {
        // TODO: Implement this method
        throw new NotImplementedException();
    }

    public void GetCurrentMeasurements(
        Action<Measurement> successCallback,
        Action<string> errorCallback = null
    )
    {
        // TODO: Implement this method
        throw new NotImplementedException();
    }

    public void GetDisruptiveByDays(
        int days,
        Action<List<Disruptive>> successCallback,
        Action<string> errorCallback = null
    )
    {
        // TODO: Implement this method
        throw new NotImplementedException();
    }

    public void GetDisruptiveByInterval(
        DateTime startTime,
        DateTime endTime,
        Action<List<Disruptive>> successCallback,
        Action<string> errorCallback = null
    )
    {
        // TODO: Implement this method
        throw new NotImplementedException();
    }

    public void GetCurrentDisruptive(
        Action<List<Disruptive>> successCallback,
        Action<string> errorCallback = null
    )
    {
        // TODO: Implement this method
        throw new NotImplementedException();
    }

    public void GetCurrentAirflow(
        Action<Airflow> successCallback,
        Action<string> errorCallback = null
    )
    {
        // TODO: Implement this method
        throw new NotImplementedException();
    }

    public void ControlLight(
        bool state,
        Action<bool> successCallback,
        Action<string> errorCallback = null
    )
    {
        // TODO: Implement this method
        throw new NotImplementedException();
    }

    public void ControlFans(
        bool state,
        Action<bool> successCallback,
        Action<string> errorCallback = null
    )
    {
        // TODO: Implement this method
        throw new NotImplementedException();
    }

    public void ControlHeater(
        float dutyCycle,
        Action<float> successCallback,
        Action<string> errorCallback = null
    )
    {
        // TODO: Implement this method
        throw new NotImplementedException();
    }

    public void GetPlantData(Action<PlantData> successCallback, Action<string> errorCallback = null)
    {
        // TODO: Implement this method
        throw new NotImplementedException();
    }

    public void ControlWaterValve(
        bool state,
        Action<bool> successCallback,
        Action<string> errorCallback = null
    )
    {
        throw new NotImplementedException();
    }
}
