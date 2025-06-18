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
        PlantModelOrigin modelOrigin = SettingsManager.PlantModelOrigin;
        modelOriginDropdown = GetComponent<TMPro.TMP_Dropdown>();
        modelOriginDropdown.value = (int)modelOrigin;
        modelOriginDropdown.onValueChanged.AddListener(OnModelOriginChanged);
    }

    private void OnModelOriginChanged(int value)
    {
        SettingsManager.PlantModelOrigin = (PlantModelOrigin)value;

        // TODO: Refresh the plant model based on the new origin
    }
}
