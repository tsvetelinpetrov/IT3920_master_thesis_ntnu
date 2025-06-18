using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlantQualityDropdown : MonoBehaviour
{
    private TMPro.TMP_Dropdown modelQualityDropdown;

    void Start()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        modelQualityDropdown = GetComponent<TMPro.TMP_Dropdown>();
        modelQualityDropdown.ClearOptions();
        modelQualityDropdown.AddOptions(
            System
                .Enum.GetNames(typeof(PlantQuality))
                .Select(name => Regex.Replace(name, "(?<!^)([A-Z])", " $1")) // insert space before capital letters, except the first
                .ToList()
        );

        PlantQuality plantQuality = SettingsManager.PlantModelQuality;
        modelQualityDropdown.value = (int)plantQuality;
        modelQualityDropdown.onValueChanged.AddListener(OnPlantQualityChanged);
    }

    private void OnPlantQualityChanged(int value)
    {
        SettingsManager.PlantModelQuality = (PlantQuality)value;

        // TODO: Refresh the plant model based on the new quality
    }
}
