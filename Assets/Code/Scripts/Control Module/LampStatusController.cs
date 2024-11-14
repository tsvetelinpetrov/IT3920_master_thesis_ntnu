using UnityEngine;

public class LampStatusController : MonoBehaviour
{
    public GameObject[] lights;

    private IDataSource dataSource;

    void Start()
    {
        // Initialize the data source
        dataSource = DataSourceFactory.GetDataSource();

        // get objects with the Tag LightGroup
        lights = GameObject.FindGameObjectsWithTag("LightGroup");

        UpdateLampStatus();
    }

    void UpdateLampStatus()
    {
        // Call GetCurrentControls
        dataSource.GetCurrentControls(
            (controls) =>
            {
                controls.LightOn = false; // for test because the api always sends true
                foreach (GameObject light in lights)
                {
                    Material lightMaterial = light.GetComponent<Renderer>().material;

                    if (controls.LightOn)
                    {
                        lightMaterial.EnableKeyword("_EMISSION");
                    }
                    else
                    {
                        lightMaterial.DisableKeyword("_EMISSION");
                    }
                }
            },
            (error) => Debug.LogError($"Failed to get control data: {error}")
        );
    }
}
