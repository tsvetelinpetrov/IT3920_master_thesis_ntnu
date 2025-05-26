using UnityEngine;

public class ValveStatusController : MonoBehaviour
{
    public ParticleSystem valveParticles;
    public AudioSource valveSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        valveParticles.Stop();
    }

    // Update is called once per frame
    void Update() { }

    void OnEnable()
    {
        EventCenter.Controls.OnValveOpen += OnValveOpen;
        EventCenter.Controls.OnValveClose += OnValveClose;
    }

    void OnDisable()
    {
        EventCenter.Controls.OnValveOpen -= OnValveOpen;
        EventCenter.Controls.OnValveClose -= OnValveClose;
    }

    void OnValveOpen()
    {
        valveParticles.Play();
        valveSound?.Play();
    }

    void OnValveClose()
    {
        valveParticles.Stop();
        valveSound?.Stop();
    }
}
