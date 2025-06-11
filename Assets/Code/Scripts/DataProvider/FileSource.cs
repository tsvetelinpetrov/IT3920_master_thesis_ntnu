using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

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
        TextAsset objFile = Resources.Load<TextAsset>("FileSourceData/mesh_low_res");

        if (objFile != null)
        {
            try
            {
                successCallback?.Invoke(objFile.text);
            }
            catch (Exception ex)
            {
                errorCallback?.Invoke($"Error reading plant model: {ex.Message}");
            }
        }
        else
        {
            errorCallback?.Invoke("Plant model file not found.");
        }
    }

    public void GetAllCurrent(
        System.Action<Current> successCallback,
        System.Action<string> errorCallback
    )
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("FileSourceData/current");
        if (jsonFile != null)
        {
            try
            {
                Current currentData = JsonConvert.DeserializeObject<Current>(jsonFile.text);
                if (currentData != null)
                {
                    Controls currentControls = new Controls
                    {
                        MeasurementTime = new DateTime(),
                        HeaterDutyCycle = GlobalSettings.Instance.HeaterDutyCycle,
                        LightOn = GlobalSettings.Instance.LightsStatus,
                        FanOn = GlobalSettings.Instance.UpperFanStatus,
                        ValveOpen = GlobalSettings.Instance.ValveStatus,
                    };
                    currentData.Controls = currentControls;
                    successCallback?.Invoke(currentData);
                }
                else
                {
                    errorCallback?.Invoke("Failed to deserialize current data.");
                }
            }
            catch (Exception ex)
            {
                errorCallback?.Invoke($"Error parsing current data: {ex.Message}");
            }
        }
        else
        {
            errorCallback?.Invoke("Current data file not found.");
        }
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
        TextAsset jsonFile = Resources.Load<TextAsset>("FileSourceData/reconstruction");
        if (jsonFile != null)
        {
            try
            {
                Airflow airflowData = JsonConvert.DeserializeObject<Airflow>(jsonFile.text);
                if (airflowData != null)
                {
                    successCallback?.Invoke(airflowData);
                }
                else
                {
                    errorCallback?.Invoke("Failed to deserialize airflow data.");
                }
            }
            catch (Exception ex)
            {
                errorCallback?.Invoke($"Error parsing airflow data: {ex.Message}");
            }
        }
        else
        {
            errorCallback?.Invoke("Airflow data file not found.");
        }
    }

    public void ControlLight(
        bool state,
        Action<bool> successCallback,
        Action<string> errorCallback = null
    )
    {
        if (state)
        {
            EventCenter.Controls.TurnOnLights();
        }
        else
        {
            EventCenter.Controls.TurnOffLights();
        }
        successCallback?.Invoke(true);
    }

    public void ControlFans(
        bool state,
        Action<bool> successCallback,
        Action<string> errorCallback = null
    )
    {
        if (state)
        {
            EventCenter.Controls.TurnOnUpperFan();
            EventCenter.Controls.TurnOnLowerFan();
        }
        else
        {
            EventCenter.Controls.TurnOffUpperFan();
            EventCenter.Controls.TurnOffLowerFan();
        }
        successCallback?.Invoke(true);
    }

    public void ControlHeater(
        float dutyCycle,
        Action<float> successCallback,
        Action<string> errorCallback = null
    )
    {
        if (dutyCycle < 0 || dutyCycle > 100)
        {
            errorCallback?.Invoke("Duty cycle must be between 0 and 100.");
            return;
        }

        GlobalSettings.Instance.HeaterDutyCycle = dutyCycle;
        successCallback?.Invoke(dutyCycle);
    }

    public void GetPlantData(Action<PlantData> successCallback, Action<string> errorCallback = null)
    {
        // Read plant data from "plant_data.json" file from the Assets/Resources folder
        // string filePath = Path.Combine(Application.dataPath, "Resources", "plant_data.json");
        // Debug.Log($"Reading plant data from: {filePath}");

        TextAsset jsonFile = Resources.Load<TextAsset>("FileSourceData/plant_data");
        if (jsonFile != null)
        {
            try
            {
                PlantData plantData = JsonConvert.DeserializeObject<PlantData>(jsonFile.text);
                if (plantData != null)
                {
                    successCallback?.Invoke(plantData);
                }
                else
                {
                    errorCallback?.Invoke("Failed to deserialize plant data.");
                }
            }
            catch (Exception ex)
            {
                errorCallback?.Invoke($"Error parsing plant data: {ex.Message}");
            }
        }
        else
        {
            errorCallback?.Invoke("Plant data file not found.");
        }
    }

    public System.Collections.IEnumerator GetPlantImage(
        System.Action<Texture2D> successCallback,
        System.Action<string> errorCallback = null
    )
    {
        Texture2D imageFile = Resources.Load<Texture2D>("FileSourceData/newest");
        if (imageFile != null)
        {
            successCallback?.Invoke(imageFile);
        }
        else
        {
            errorCallback?.Invoke("Plant image file not found.");
        }

        yield return null;
    }

    public void ControlWaterValve(
        bool state,
        Action<bool> successCallback,
        Action<string> errorCallback = null
    )
    {
        if (state)
        {
            EventCenter.Controls.OpenValve();
        }
        else
        {
            EventCenter.Controls.CloseValve();
        }
        successCallback?.Invoke(true);
    }
}
