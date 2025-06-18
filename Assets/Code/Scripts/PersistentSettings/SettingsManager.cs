using System.Collections.Generic;
using UnityEngine;

public enum WorkingMode
{
    Offline,
    Online,
}

public enum PlantModelOrigin
{
    None,
    DataSource,
    DummyPlant,
}

public enum DebugMode
{
    Off,
    On,
}

public enum PlantQuality
{
    Low,
    High,
}

public static class SettingsManager
{
    // Default values
    private static Dictionary<string, string> defaultSettings = new Dictionary<string, string>
    {
        { "WorkingMode", WorkingMode.Online.ToString() },
        { "PlantModelOrigin", PlantModelOrigin.DataSource.ToString() },
        { "PlantModelQuality", PlantQuality.Low.ToString() },
        { "RefreshRate", "10" },
        { "APIAddress", "http://10.53.8.177:8000/" },
        { "DebugMode", DebugMode.Off.ToString() },
    };

    static SettingsManager()
    {
        InitializeDefaults();
    }

    private static void InitializeDefaults()
    {
        foreach (var setting in defaultSettings)
        {
            if (!PlayerPrefs.HasKey(setting.Key))
            {
                string value = setting.Value is string ? setting.Value : setting.Value.ToString();
                PlayerPrefs.SetString(setting.Key, value);
            }
        }

        PlayerPrefs.Save();
    }

    // Generic Enum getter/setter
    private static T GetEnum<T>(string key)
        where T : System.Enum
    {
        string value = PlayerPrefs.GetString(key, defaultSettings[key].ToString());
        return (T)System.Enum.Parse(typeof(T), value);
    }

    private static void SetEnum<T>(string key, T value)
        where T : System.Enum
    {
        PlayerPrefs.SetString(key, value.ToString());
        PlayerPrefs.Save();
    }

    // Int getter/setter
    private static int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key, int.Parse(defaultSettings[key]));
    }

    private static void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    // String getter/setter
    private static string GetString(string key)
    {
        return PlayerPrefs.GetString(key, (string)defaultSettings[key]);
    }

    private static void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    // === Convenience Accessors ===
    public static WorkingMode WorkingMode
    {
        get => GetEnum<WorkingMode>("WorkingMode");
        set => SetEnum("WorkingMode", value);
    }

    public static PlantModelOrigin PlantModelOrigin
    {
        get => GetEnum<PlantModelOrigin>("PlantModelOrigin");
        set => SetEnum("PlantModelOrigin", value);
    }

    public static PlantQuality PlantModelQuality
    {
        get => GetEnum<PlantQuality>("PlantModelQuality");
        set => SetEnum("PlantModelQuality", value);
    }

    public static DebugMode DebugMode
    {
        get => GetEnum<DebugMode>("DebugMode");
        set => SetEnum("DebugMode", value);
    }

    public static int RefreshRate
    {
        get => GetInt("RefreshRate");
        set => SetInt("RefreshRate", value);
    }

    public static string APIAddress
    {
        get => GetString("APIAddress");
        set => SetString("APIAddress", value);
    }
}
