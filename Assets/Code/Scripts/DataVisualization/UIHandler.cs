using System;
using TMPro;
using UnityEngine;

public class DateInputManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField startDateInput;

    [SerializeField]
    private TMP_InputField endDateInput;

    void Start()
    {
        // Set input validation
        startDateInput.contentType = TMP_InputField.ContentType.Standard;
        endDateInput.contentType = TMP_InputField.ContentType.Standard;

        // Add listeners
        startDateInput.onEndEdit.AddListener(ValidateDate);
        endDateInput.onEndEdit.AddListener(ValidateDate);
    }

    private void ValidateDate(string input)
    {
        if (DateTime.TryParse(input, out DateTime date))
        {
            Debug.Log($"Valid date: {date:yyyy-MM-dd}");
            // Call your API here with the date
        }
        else
        {
            Debug.LogWarning("Invalid date format");
        }
    }
}
