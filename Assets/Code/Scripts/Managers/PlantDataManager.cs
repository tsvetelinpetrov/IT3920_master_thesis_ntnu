using UnityEngine;
using UnityEngine.UI;

public class PlantDataManager : MonoBehaviour
{
    [Header("Plant Data Settings")]
    [Tooltip("Number of diseases to show in the UI")]
    [Range(1, 5)]
    public int diseasesToShow = 1; // Number of diseases to show
    public Transform plantDataContainer;
    public Image plantImage;
    public TMPro.TMP_Text plantNameText;
    public TMPro.TMP_Text plantDescriptionText;
    public TMPro.TMP_Text plantLightConditionText;
    public TMPro.TMP_Text plantSoilConditionText;
    public TMPro.TMP_Text plantWateringConditionText;
    public GameObject plantDiseasePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetPlantData();
        DownloadImage();
    }

    // Update is called once per frame
    void Update() { }

    private void GetPlantData()
    {
        IDataSource dataSource = DataSourceFactory.GetDataSource();

        dataSource.GetPlantData(
            (plantData) =>
            {
                plantNameText.text = plantData.classifications[0]?.name;
                plantDescriptionText.text = plantData
                    .classifications[0]
                    ?.details
                    ?.description
                    ?.value;
                plantLightConditionText.text = plantData
                    .classifications[0]
                    ?.details
                    ?.bestLightCondition;
                plantSoilConditionText.text = plantData.classifications[0]?.details?.bestSoilType;
                plantWateringConditionText.text = plantData
                    .classifications[0]
                    ?.details
                    ?.bestWatering;

                int diseaseI = 0;
                foreach (var disease in plantData.diseases)
                {
                    if (diseaseI >= diseasesToShow)
                        break;

                    GameObject diseaseObject = Instantiate(plantDiseasePrefab, plantDataContainer);

                    // Get the disease name and description text components from the prefab
                    TMPro.TMP_Text diseaseNameText = diseaseObject
                        .transform.Find("Name")
                        .GetComponent<TMPro.TMP_Text>();
                    TMPro.TMP_Text diseaseProbabilityText = diseaseObject
                        .transform.Find("Probability")
                        .GetComponent<TMPro.TMP_Text>();
                    TMPro.TMP_Text diseaseDescriptionText = diseaseObject
                        .transform.Find("Val")
                        .GetComponent<TMPro.TMP_Text>();

                    // Set the text values for the disease name and description
                    diseaseNameText.text = disease.name;
                    diseaseProbabilityText.text += $"{disease.probability * 100}%";
                    diseaseDescriptionText.text = disease.details.description;

                    diseaseI++;
                }
            },
            (error) => Debug.LogError($"Error fetching plant data: {error}")
        );
    }

    private void DownloadImage()
    {
        IDataSource dataSource = DataSourceFactory.GetDataSource();

        StartCoroutine(
            dataSource.GetPlantImage(
                (texture) =>
                {
                    if (texture != null)
                    {
                        plantImage.sprite = Sprite.Create(
                            texture,
                            new Rect(0, 0, texture.width, texture.height),
                            new Vector2(0.5f, 0.5f)
                        );
                    }
                    else
                    {
                        Debug.LogError("Failed to load plant image.");
                    }
                },
                (error) => Debug.LogError($"Error fetching plant image: {error}")
            )
        );
    }
}
