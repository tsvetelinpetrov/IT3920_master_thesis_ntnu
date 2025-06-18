using UnityEngine;

public class FansControllerModule : MonoBehaviour
{
    public GameObject upperFan;
    public GameObject lowerFan;

    public ParticleSystem upperFanParticles;
    public ParticleSystem lowerFanParticles;

    public AudioSource fanSound;

    public float rotationSpeed = 200f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upperFanParticles.Stop();
        lowerFanParticles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalParameters.Instance.UpperFanStatus)
        {
            // Rotate the upper fan around its own X-axis
            upperFan.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime, Space.World);
        }

        if (GlobalParameters.Instance.LowerFanStatus)
        {
            // Rotate the upper fan around its own X-axis
            lowerFan.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime, Space.World);
        }
    }

    void OnEnable()
    {
        EventCenter.Controls.OnTurnOnUpperFan += OnTurnOnUpperFan;
        EventCenter.Controls.OnTurnOffUpperFan += OnTurnOffUpperFan;
        EventCenter.Controls.OnTurnOnLowerFan += OnTurnOnLowerFan;
        EventCenter.Controls.OnTurnOffLowerFan += OnTurnOffLowerFan;
    }

    void OnDisable()
    {
        EventCenter.Controls.OnTurnOnUpperFan -= OnTurnOnUpperFan;
        EventCenter.Controls.OnTurnOffUpperFan -= OnTurnOffUpperFan;
        EventCenter.Controls.OnTurnOnLowerFan -= OnTurnOnLowerFan;
        EventCenter.Controls.OnTurnOffLowerFan -= OnTurnOffLowerFan;
    }

    void OnTurnOnUpperFan()
    {
        upperFanParticles.Play();
        fanSound?.Play();
    }

    void OnTurnOffUpperFan()
    {
        upperFanParticles.Stop();
        fanSound?.Stop();
    }

    void OnTurnOnLowerFan()
    {
        lowerFanParticles.Play();
    }

    void OnTurnOffLowerFan()
    {
        lowerFanParticles.Stop();
    }
}
