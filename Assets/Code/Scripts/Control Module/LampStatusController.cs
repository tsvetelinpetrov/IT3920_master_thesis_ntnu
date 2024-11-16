using UnityEngine;

public class LampStatusController : MonoBehaviour
{
    public GameObject[] Emissions;
    public Light[] lampLights;
    private IDataSource dataSource;

    void Start()
    {
        // Initialize the data source
        dataSource = DataSourceFactory.GetDataSource();

        // get objects with the Tag LightGroup for emissions
        Emissions = GameObject.FindGameObjectsWithTag("LightGroup");

        GameObject lightingsGroup = GameObject.Find("Lightings");
        if (lightingsGroup != null)
        {
            lampLights = new Light[4];
            for (int i = 1; i <= 4; i++)
            {
                Transform spotLight = lightingsGroup.transform.Find($"Spot Light {i}");
                if (spotLight != null)
                {
                    lampLights[i - 1] = spotLight.GetComponent<Light>();
                }
            }
        }
    }

    void OnEnable()
    {
        // Register to the event
        EventCenter.Controls.OnTurnOnLights += TurnOnLights;
        EventCenter.Controls.OnTurnOffLights += TurnOffLights;
    }

    void OnDisable()
    {
        // Unregister from the event
        EventCenter.Controls.OnTurnOnLights -= TurnOnLights;
        EventCenter.Controls.OnTurnOffLights -= TurnOffLights;
    }

    void TurnOnLights()
    {
        foreach (GameObject light in Emissions)
        {
            Material lightMaterial = light.GetComponent<Renderer>().material;

            lightMaterial.EnableKeyword("_EMISSION");
        }

        foreach (Light spotlight in lampLights)
        {
            if (spotlight != null)
            {
                spotlight.enabled = true;
            }
        }
    }

    void TurnOffLights()
    {
        foreach (GameObject light in Emissions)
        {
            Material lightMaterial = light.GetComponent<Renderer>().material;

            lightMaterial.DisableKeyword("_EMISSION");
        }

        foreach (Light spotlight in lampLights)
        {
            if (spotlight != null)
            {
                spotlight.enabled = false;
            }
        }
    }
}
