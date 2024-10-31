using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    public static GlobalSettings Instance { get; private set; }

    // Global settings (vars that should be accessible from any script)
    public string ApiUrl = "https://greenhouse-data-api-ddefcwbncabfftbv.canadacentral-01.azurewebsites.net/";
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
