using UnityEngine;

// Struct to define the operating modes for the Digital Twin (Standalone or API)
public enum OperatingMode
{
    Standalone,
    Realtime,
}

public class GlobalSettings : MonoBehaviour
{
    public static GlobalSettings Instance { get; private set; }

    // Global settings (vars that should be accessible from any script)
    public OperatingMode OperatingMode = OperatingMode.Realtime;
    public string ApiUrl =
        "https://greenhouse-data-api-ddefcwbncabfftbv.canadacentral-01.azurewebsites.net/";
    public DataSourceType DataSourceType = DataSourceType.Api;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
