using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataProvider : MonoBehaviour
{

    void Start()
    {
        IDataSource dataSource = DataSourceFactory.GetDataSource();
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
