using UnityEngine;
using UnityEngine.UI;

public class DataRefreshRateSelector : MonoBehaviour
{
    private Slider refreshRateSlider;
    private TMPro.TMP_Text refreshRateText;

    void Start()
    {
        // Get child components
        refreshRateSlider = transform.Find("RefreshRateSlider").GetComponent<Slider>();
        refreshRateText = transform.Find("RefreshRateText").GetComponent<TMPro.TMP_Text>();

        // Initialize the slider value and text
        int currentRate = SettingsManager.RefreshRate;
        refreshRateSlider.value = currentRate;
        refreshRateText.text = $"{currentRate}s";
        refreshRateSlider.onValueChanged.AddListener(OnRefreshRateChanged);
    }

    private void OnRefreshRateChanged(float value)
    {
        int newRate = Mathf.RoundToInt(value);
        SettingsManager.RefreshRate = newRate;

        // Update the text to reflect the new rate
        refreshRateText.text = $"{newRate}s";

        // TODO: Refresh the data fetching mechanism in GreenhouseManager or other relevant components
    }
}
