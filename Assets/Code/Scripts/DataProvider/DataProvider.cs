using Newtonsoft.Json;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DataProvider : MonoBehaviour
{

    // Text object to display the data
    public TMP_Text DebugText;

    void Start()
    {

    }

    // Get plant model from data source
    public void GetPlantModel()
    {
        IDataSource dataSource = DataSourceFactory.GetDataSource();
        dataSource.GetPlantObjModel(
            (model) =>
            {
                // Put first 1000 characters of the model into the debug text
                DebugText.text = model.Substring(0, 1000);
                //PlantRenderer plantRenderer = new PlantRenderer();
                //plantRenderer.renderPlant(model);
            }
        );
    }

    // Get controls by days from data source
    public void GetControlsByDays()
    {
        IDataSource dataSource = DataSourceFactory.GetDataSource();
        dataSource.GetControlsByDays(30,
            (controls) =>
            {
                // Get controls as json string
                string jsonString = JsonConvert.SerializeObject(controls, Formatting.Indented);

                Debug.Log(jsonString);
                DebugText.text = jsonString;
            }
        );
    }

}
