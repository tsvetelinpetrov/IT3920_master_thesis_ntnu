using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlantDataManager : MonoBehaviour
{
    public Image plantImage;
    public TMPro.TMP_Text plantNameText;
    public TMPro.TMP_Text plantDescriptionText;
    public TMPro.TMP_Text plantLightConditionText;
    public TMPro.TMP_Text plantSoilConditionText;
    public TMPro.TMP_Text plantWateringConditionText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetPlantData();
        StartCoroutine(DownloadImage());
    }

    // Update is called once per frame
    void Update() { }

    private void GetPlantData()
    {
        IDataSource dataSource = DataSourceFactory.GetDataSource();

        dataSource.GetPlantData(
            (plantData) =>
            {
                plantNameText.text = plantData.name;
                plantDescriptionText.text = plantData.description;
                plantLightConditionText.text = plantData.best_light_condition;
                plantSoilConditionText.text = plantData.best_soil_type;
                plantWateringConditionText.text = plantData.best_watering;
            },
            (error) => Debug.LogError($"Error fetching plant data: {error}")
        );
    }

    IEnumerator DownloadImage()
    {
        string imageUrl = GlobalSettings.Instance.ApiUrl + "/image/newest";

        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            yield return webRequest.SendWebRequest();

            if (
                webRequest.result == UnityWebRequest.Result.ConnectionError
                || webRequest.result == UnityWebRequest.Result.ProtocolError
            )
            {
                Debug.LogError($"Error downloading image: {webRequest.error}");
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
                plantImage.sprite = Sprite.Create(
                    texture,
                    new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f)
                );
            }
        }

        yield return null;
    }
}
