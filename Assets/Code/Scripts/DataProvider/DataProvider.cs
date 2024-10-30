using System.Collections.Generic;
using UnityEngine;

public class DataProvider : MonoBehaviour
{
    public DataSourceType ObtainFromApi = DataSourceType.Api;
    public string ApiUrl = "https://greenhouse-data-api-ddefcwbncabfftbv.canadacentral-01.azurewebsites.net/";

    void Start()
    {
        IDataSource dataSource = DataSourceFactory.GetDataSource(ObtainFromApi, ApiUrl);
        // dataSource.GetControlsByDays(50,
        //     (controls) =>
        //     {
        //         Debug.Log(controls.Count);
        //     }
        // );

        dataSource.GetPlantObjModel(
            (model) =>
            {
                PlantRenderer plantRenderer = new PlantRenderer();
                plantRenderer.renderPlant(model);
            }
        );
    }

}
