using UnityEngine;

public class FPSMonitor : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text fpsText;
    const int frameRange = 60;
    float[] fpsBuffer;
    int fpsBufferIndex;

    void InitializeBuffer()
    {
        if (fpsBuffer == null || fpsBuffer.Length != frameRange)
        {
            fpsBuffer = new float[frameRange];
            fpsBufferIndex = 0;
        }
    }

    void Update()
    {
        InitializeBuffer();
        fpsBuffer[fpsBufferIndex++] = 1f / Time.unscaledDeltaTime;

        if (fpsBufferIndex >= frameRange)
            fpsBufferIndex = 0;

        float sum = 0f;
        for (int i = 0; i < frameRange; i++)
        {
            sum += fpsBuffer[i];
        }

        float averageFPS = sum / frameRange;
        fpsText.text = $"FPS: {averageFPS:0.}";
    }
}
