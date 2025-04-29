using System.Collections.Generic;
using System.IO;
using System.Text;
using Dummiesman;
using UnityEngine;

public class GreenhouseManager : MonoBehaviour
{
    public Material plantMaterial;
    public GameObject plantHolder;
    public GameObject dummyPlant;
    private LightToggleButton lightButton;
    private FanToggleButton fanButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GlobalSettings.Instance.OperatingMode == OperatingMode.Realtime)
        {
            // Each x seconds, get the current data
            InvokeRepeating(
                "InitializeAllCurrentData",
                0,
                GlobalSettings.Instance.CurrentDataRefreshRate
            );

            // Get the plant model
            if (GlobalSettings.Instance.ObtainPlantModelFromApi)
            {
                GetPlantModel();
            }
            else
            {
                dummyPlant.SetActive(true);
            }

            GetCurrentAirflow();
        }
        else
        {
            // TODO: Implement the Standalone mode logic if needed (probably we can skip it and just use the API/File mode)
        }
    }

    // Update is called once per frame
    void Update() { }

    private void InitializeAllCurrentData()
    {
        IDataSource dataSource = DataSourceFactory.GetDataSource();

        EventCenter.Controls.ChangeRefreshingStatus(true);
        EventCenter.Measurements.ChangeRefreshingStatus(true);

        // Call GetAllCurrent
        dataSource.GetAllCurrent(
            (current) =>
            {
                ProcessControlsData(current.Controls);
                ProcessMeasurementsData(current.Measurements);
                ProcessDisruptiveData(current.Disruptive);
                EventCenter.Controls.ChangeRefreshingStatus(false);
                EventCenter.Measurements.ChangeRefreshingStatus(false);
            },
            (error) =>
            {
                Debug.LogError($"Failed to get current data: {error}");
                EventCenter.Controls.ChangeRefreshingStatus(false);
                EventCenter.Measurements.ChangeRefreshingStatus(false);
            }
        );
    }

    private void ProcessControlsData(Controls controls)
    {
        EventCenter.Controls.ChangeControls(controls);
        if (controls.LightOn)
        {
            EventCenter.Controls.TurnOnLights();
        }
        else
        {
            EventCenter.Controls.TurnOffLights();
        }
        if (
            controls.FanOn
            && !GlobalSettings.Instance.UpperFanStatus
            && !GlobalSettings.Instance.LowerFanStatus
        )
        {
            EventCenter.Controls.TurnOnUpperFan();
            EventCenter.Controls.TurnOnLowerFan();
        }
        else if (
            !controls.FanOn
            && (GlobalSettings.Instance.UpperFanStatus || GlobalSettings.Instance.LowerFanStatus)
        )
        {
            EventCenter.Controls.TurnOffUpperFan();
            EventCenter.Controls.TurnOffLowerFan();
        }

        if (controls.ValveOpen && !GlobalSettings.Instance.ValveStatus)
        {
            EventCenter.Controls.OpenValve();
        }
        else if (!controls.ValveOpen && GlobalSettings.Instance.ValveStatus)
        {
            EventCenter.Controls.CloseValve();
        }
        // initialize light button value
        lightButton = FindFirstObjectByType<LightToggleButton>();
        lightButton.UpdateLightState(controls.LightOn);
        // initialize fan button value
        fanButton = FindFirstObjectByType<FanToggleButton>();
        if (fanButton != null)
        {
            fanButton.UpdateFanState(controls.FanOn);
        }
    }

    private void ProcessMeasurementsData(Measurement measurements)
    {
        EventCenter.Measurements.ChangeMeasurements(measurements);
    }

    private void ProcessDisruptiveData(List<Disruptive> disruptive)
    {
        // TODO: Implement the logic to process the disruptive data
    }

    private void GetPlantModel()
    {
        IDataSource dataSource = DataSourceFactory.GetDataSource();
        dataSource.GetPlantObjModel(
            (model) =>
            {
                var textStream = new MemoryStream(Encoding.UTF8.GetBytes(model));
                var loadedObj = new OBJLoader().Load(textStream);

                // Apply the material to the plant
                foreach (var meshRenderer in loadedObj.GetComponentsInChildren<MeshRenderer>())
                {
                    meshRenderer.material = plantMaterial;
                }

                // Set the plant position in plantHolder
                loadedObj.transform.SetParent(plantHolder.transform);
                loadedObj.transform.localPosition = Vector3.zero;
                loadedObj.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            },
            false,
            (error) =>
            {
                Debug.LogError($"Failed to get plant model: {error}");
            }
        );

        // TODO: Experiment and see which way is faster
        // string url = GlobalSettings.Instance.ApiUrl + "mesh/low_res";
        // var www = new WWW(url);
        // while (!www.isDone)
        //     System.Threading.Thread.Sleep(1);
        // var textStream = new MemoryStream(Encoding.UTF8.GetBytes(www.text));
        // var loadedObj = new OBJLoader().Load(textStream);
    }

    private void GetCurrentAirflow()
    {
        IDataSource dataSource = DataSourceFactory.GetDataSource();
        dataSource.GetCurrentAirflow(
            (airflow) =>
            {
                EventCenter.Airflow.NewAirflowData(airflow);
            },
            (error) =>
            {
                Debug.LogError($"Failed to get airflow data: {error}");
            }
        );
    }
}
