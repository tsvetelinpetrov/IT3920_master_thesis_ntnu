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
