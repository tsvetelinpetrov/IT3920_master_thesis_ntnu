using System;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class DataProvider : MonoBehaviour
{
    // Text object to display the data
    public TMP_Text DebugText;

    void Start() { }

    public void GetPlantModel()
    {
        IDataSource dataSource = DataSourceFactory.GetDataSource();

        System.Action<string> callback = (model) =>
        {
            DebugText.text = model.Substring(0, 1000);
        };

        dataSource.GetPlantObjModel(
            (model) =>
            {
                Debug.Log(model);
                DebugText.text = model.Substring(0, 1000);
            },
            false,
            (error) =>
            {
                // This will be called if the API request fails or if the file reading fails.
                // Can be used to display an error message to the user and/or to enable/disable UI elements.
                Debug.Log(error);
            }
        );
    }

    public void GetControlsByInterval()
    {
        IDataSource dataSource = DataSourceFactory.GetDataSource();

        DateTime startTime = new DateTime(2024, 11, 11, 10, 0, 0, DateTimeKind.Utc);
        DateTime endTime = new DateTime(2024, 11, 11, 11, 0, 0, DateTimeKind.Utc);

        dataSource.GetControlsByInterval(
            startTime,
            endTime,
            (controls) =>
            {
                string jsonString = JsonConvert.SerializeObject(controls, Formatting.None);

                Debug.Log(jsonString);
                DebugText.text = jsonString;

                foreach (Controls control in controls)
                {
                    Debug.Log(control.MeasurementTime);
                }
            },
            (error) =>
            {
                // This will be called if the API request fails or if the file reading fails.
                // Can be used to display an error message to the user and/or to enable/disable UI elements.
                Debug.Log(error);
            }
        );
    }

    public void GetControlsByDays()
    {
        IDataSource dataSource = DataSourceFactory.GetDataSource();
        dataSource.GetControlsByDays(
            30,
            (controls) =>
            {
                string jsonString = JsonConvert.SerializeObject(controls, Formatting.None);

                Debug.Log(jsonString);
                DebugText.text = jsonString;

                foreach (Controls control in controls)
                {
                    Debug.Log(control.MeasurementTime);
                }
            },
            (error) =>
            {
                // This will be called if the API request fails or if the file reading fails.
                // Can be used to display an error message to the user and/or to enable/disable UI elements.
                Debug.Log(error);
            }
        );
    }

    public void GetCurrentControls()
    {
        IDataSource dataSource = DataSourceFactory.GetDataSource();
        dataSource.GetCurrentControls(
            (controls) =>
            {
                string jsonString = JsonConvert.SerializeObject(controls, Formatting.None);

                Debug.Log(jsonString);
                DebugText.text = jsonString;

                Debug.Log(controls.MeasurementTime);
            },
            (error) =>
            {
                // This will be called if the API request fails or if the file reading fails.
                // Can be used to display an error message to the user and/or to enable/disable UI elements.
                Debug.Log(error);
            }
        );
    }
}
