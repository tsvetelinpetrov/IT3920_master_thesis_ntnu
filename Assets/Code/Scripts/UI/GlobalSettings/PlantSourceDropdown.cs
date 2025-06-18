using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlantSourceDropdown : MonoBehaviour
{
    private TMPro.TMP_Dropdown modelOriginDropdown;

    void Start()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        modelOriginDropdown = GetComponent<TMPro.TMP_Dropdown>();
        modelOriginDropdown.ClearOptions();
        modelOriginDropdown.AddOptions(
            System
                .Enum.GetNames(typeof(PlantModelOrigin))
                .Select(name => Regex.Replace(name, "(?<!^)([A-Z])", " $1")) // insert space before capital letters, except the first
                .ToList()
        );

        PlantModelOrigin modelOrigin = SettingsManager.PlantModelOrigin;
        modelOriginDropdown.value = (int)modelOrigin;
        modelOriginDropdown.onValueChanged.AddListener(OnModelOriginChanged);
    }

    private void OnModelOriginChanged(int value)
    {
        SettingsManager.PlantModelOrigin = (PlantModelOrigin)value;

        // TODO: Refresh the plant model based on the new origin
    }
}
