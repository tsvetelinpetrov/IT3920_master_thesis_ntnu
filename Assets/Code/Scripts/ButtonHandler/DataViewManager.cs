using UnityEngine;
using UnityEngine.UI;

public class DataViewManager : MonoBehaviour
{
    [SerializeField]
    private GameObject liveDataCanvas;

    [SerializeField]
    private GameObject historicalDataCanvas;

    [SerializeField]
    private Button liveDataButton;

    [SerializeField]
    private Button historicalDataButton;

    private void Start()
    {
        liveDataButton.onClick.AddListener(ShowLiveData);
        historicalDataButton.onClick.AddListener(ShowHistoricalData);

        // Default view
        ShowLiveData();
    }

    public void ShowLiveData()
    {
        liveDataCanvas.SetActive(true);
        historicalDataCanvas.SetActive(false);

        // Visual feedback
        liveDataButton.interactable = false;
        historicalDataButton.interactable = true;
    }

    public void ShowHistoricalData()
    {
        liveDataCanvas.SetActive(false);
        historicalDataCanvas.SetActive(true);

        // Visual feedback
        liveDataButton.interactable = true;
        historicalDataButton.interactable = false;
    }
}
