using System;
using UnityEngine;
using UnityEngine.UI;

public class APIAddressSetter : MonoBehaviour
{
    private TMPro.TMP_InputField apiAddressInputField;
    private Button saveButton;

    void Start()
    {
        // Get the input field and button components
        apiAddressInputField = transform
            .Find("APIAddressInputField")
            .GetComponent<TMPro.TMP_InputField>();
        saveButton = transform.Find("APIAddressSetButton").GetComponent<Button>();

        // Set the initial value from settings
        apiAddressInputField.text = SettingsManager.APIAddress;

        // Add listener for the save button
        saveButton.onClick.AddListener(OnSaveButtonClicked);
    }

    public void OnSaveButtonClicked()
    {
        // Get the new API address from the input field
        string newApiAddress = apiAddressInputField?.text;

        if (string.IsNullOrEmpty(newApiAddress))
        {
            Debug.LogError("API Address is empty.");
            return;
        }

        // Validate the URL format (basic validation)
        if (Uri.IsWellFormedUriString(newApiAddress, UriKind.Absolute))
        {
            // Save the new API address to settings
            SettingsManager.APIAddress = newApiAddress;
            Debug.Log("API Address updated to: " + newApiAddress);
        }
        else
        {
            Debug.LogError("Invalid API Address format: " + newApiAddress);
        }
    }
}
