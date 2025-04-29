using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LightToggleButton : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private Image buttonImage;

    [SerializeField]
    private TextMeshProUGUI buttonText; // Or Text component if using Unity UI Text

    [SerializeField]
    private Color onColor = new Color(1f, 0.9f, 0.5f); // Warm yellow for ON

    [SerializeField]
    private Color offColor = new Color(0.3f, 0.3f, 0.35f); // Dark gray for OFF

    private bool currentLightState = true;
    private bool isProcessing = false;

    void Start()
    {
        // Set up button click listener
        if (button == null)
            button = GetComponent<Button>();

        button.onClick.AddListener(ToggleLight);

        // Initialize button state
        UpdateButtonVisuals();
    }

    public void SetInitialState(bool lightIsOn)
    {
        currentLightState = lightIsOn;
        UpdateButtonVisuals();
    }

    private void ToggleLight()
    {
        if (isProcessing)
            return; // Prevent multiple clicks while request is processing

        isProcessing = true;

        // Set button to a disabled state
        button.interactable = false;

        // Toggle the light state (opposite of current)
        bool newState = !currentLightState;
        IDataSource dataSource = DataSourceFactory.GetDataSource();
        // Call API to change light state
        dataSource.ControlLight(
            newState,
            success =>
            {
                // Update internal state after successful API call
                currentLightState = newState;
                isProcessing = false;
                button.interactable = true;

                // Update button visuals
                UpdateButtonVisuals();

                Debug.Log($"Light turned {(newState ? "ON" : "OFF")} successfully");
            },
            error =>
            {
                // API call failed, revert to previous state
                isProcessing = false;
                button.interactable = true;

                // Keep visuals the same as before
                UpdateButtonVisuals();

                Debug.LogError($"Failed to toggle light: {error}");
            }
        );
    }

    private void UpdateButtonVisuals()
    {
        // Update button color
        if (buttonImage != null)
            buttonImage.color = currentLightState ? onColor : offColor;

        // Update button text
        if (buttonText != null)
            buttonText.text = currentLightState ? "Light: ON" : "Light: OFF";
    }

    public void UpdateLightState(bool newState)
    {
        if (currentLightState != newState)
        {
            currentLightState = newState;
            UpdateButtonVisuals();
        }
    }
}
