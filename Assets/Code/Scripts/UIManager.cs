using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Measurements UI
    public TMPro.TMP_Text temperatureText;
    public TMPro.TMP_Text moistureText;
    public TMPro.TMP_Text co2Text;
    public TMPro.TMP_Text lightIntensityText;
    public TMPro.TMP_Text humidityText;
    public TMPro.TMP_Text tankLevelText;
    public TMPro.TMP_Text measurementsRefreshingText;

    // Controls UI
    public TMPro.TMP_Text heaterDutyCycleText;
    public TMPro.TMP_Text lightsStatusText;
    public TMPro.TMP_Text upperFanStatusText;
    public TMPro.TMP_Text lowerFanStatusText;
    public TMPro.TMP_Text valveStatusText;
    public TMPro.TMP_Text controlsRefreshingText;

    void OnEnable()
    {
        EventCenter.Measurements.OnMeasurementsChange += UpdateMeasurements;
        EventCenter.Measurements.OnMeasurementsRefreshingStatusChange += SetRefreshingText;
        EventCenter.Controls.OnControlsChanged += UpdateControls;
        EventCenter.Controls.OnControlsRefreshingStatusChange += SetControlsRefreshingText;
    }

    void OnDisable()
    {
        EventCenter.Measurements.OnMeasurementsChange -= UpdateMeasurements;
        EventCenter.Measurements.OnMeasurementsRefreshingStatusChange -= SetRefreshingText;
        EventCenter.Controls.OnControlsChanged -= UpdateControls;
        EventCenter.Controls.OnControlsRefreshingStatusChange -= SetControlsRefreshingText;
    }

    private void UpdateMeasurements(Measurement measurement)
    {
        temperatureText.text = $"{measurement.Temperature}°C";
        moistureText.text = $"{measurement.Moisture}";
        co2Text.text = $"{measurement.CO2} ppm";
        lightIntensityText.text = $"{measurement.LightIntensity} lux";
        humidityText.text = $"{measurement.Humidity}%";
        tankLevelText.text = $"{measurement.TankLevel}%";
    }

    public void SetRefreshingText(bool refreshing)
    {
        measurementsRefreshingText.text = refreshing ? "Refreshing..." : "";
    }

    private void UpdateControls(Controls controls)
    {
        heaterDutyCycleText.text = $"{(int)(controls.HeaterDutyCycle * 100)}%";
        lightsStatusText.text = controls.LightOn ? "On" : "Off";
        upperFanStatusText.text = controls.FanOn ? "On" : "Off";
        lowerFanStatusText.text = controls.FanOn ? "On" : "Off";
        valveStatusText.text = controls.ValveOpen ? "Open" : "Closed";
    }

    public void SetControlsRefreshingText(bool refreshing)
    {
        controlsRefreshingText.text = refreshing ? "Refreshing..." : "";
    }
}
