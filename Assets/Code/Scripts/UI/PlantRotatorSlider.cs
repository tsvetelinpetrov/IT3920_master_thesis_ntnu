using UnityEngine;
using UnityEngine.UI;

public class PlantRotatorSlider : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the plant model that will be rotated.")]
    private Transform plantModel;
    private Slider plantRotatorSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        plantRotatorSlider = GetComponent<Slider>();

        plantRotatorSlider?.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDestroy()
    {
        if (plantRotatorSlider != null)
        {
            plantRotatorSlider.onValueChanged.RemoveListener(OnValueChanged);
        }
    }

    // Update is called once per frame
    void Update() { }

    private void OnValueChanged(float value)
    {
        if (plantModel == null)
            return;

        plantModel.localRotation = Quaternion.Euler(0, value * 360f, 0);
    }
}
