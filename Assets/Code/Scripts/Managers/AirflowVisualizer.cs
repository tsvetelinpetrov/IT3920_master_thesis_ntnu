using System;
using System.Collections.Generic;
using UnityEngine;

public class AirflowVisualizer : MonoBehaviour
{
    public GameObject windArrowPrefab;
    public Gradient windColorGradient;
    public int xArrowCount = 10;
    public int yArrowCount = 10;
    public int zArrowCount = 10;
    public bool active = true;

    private List<GameObject> windArrows = new List<GameObject>();
    private float minMagnitude;
    private float maxMagnitude;

    void OnEnable()
    {
        EventCenter.Airflow.OnNewAirflowData += OnNewAirflowData;
    }

    void OnDisable()
    {
        EventCenter.Airflow.OnNewAirflowData -= OnNewAirflowData;
    }

    // Update is called once per frame
    void Update() { }

    void OnNewAirflowData(Airflow airflow)
    {
        // Debug.Log("New airflow data received: " + airflow.Data.Count);

        CalculateMagnitudeLimits(airflow);
        // Debug.Log("Min magnitude: " + minMagnitude);
        // Debug.Log("Max magnitude: " + maxMagnitude);
        DrawArrows(airflow);
    }

    private void CalculateMagnitudeLimits(Airflow airData)
    {
        minMagnitude = float.MaxValue;
        maxMagnitude = float.MinValue;

        for (int x = 0; x < xArrowCount; x++)
        {
            for (int y = 0; y < yArrowCount; y++)
            {
                for (int z = 0; z < zArrowCount; z++)
                {
                    Vector3 dir = new Vector3(
                        (float)airData.Data[x][y][z][1],
                        (float)airData.Data[x][y][z][2],
                        (float)airData.Data[x][y][z][3]
                    );
                    float magnitude = dir.magnitude;
                    if (magnitude < minMagnitude)
                    {
                        minMagnitude = magnitude;
                    }
                    if (magnitude > maxMagnitude)
                    {
                        maxMagnitude = magnitude;
                    }
                }
            }
        }
    }

    void DrawArrows(Airflow airData)
    {
        foreach (GameObject arrow in windArrows)
        {
            Destroy(arrow);
        }

        windArrows.Clear();

        float x0 = transform.position.x - transform.localScale.x / 2f;
        float y0 = transform.position.y + transform.localScale.y / 2f;
        float z0 = transform.position.z - transform.localScale.z / 2f;

        float x1 = transform.position.x + transform.localScale.x / 2f;
        float y1 = transform.position.y - transform.localScale.y / 2f;
        float z1 = transform.position.z + transform.localScale.z / 2f;

        float xStep = (x1 - x0) / xArrowCount;
        float yStep = (y1 - y0) / yArrowCount;
        float zStep = (z1 - z0) / zArrowCount;

        for (int x = 0; x < xArrowCount; x++)
        {
            float xCoord = x0 + (x * xStep);
            for (int y = 0; y < yArrowCount; y++)
            {
                float yCoord = y0 + (y * yStep);
                for (int z = 0; z < zArrowCount; z++)
                {
                    float zCoord = z0 + (z * zStep);

                    int sampleX = (int)(x * (airData.Data.Count) / (xArrowCount));
                    int sampleY = (int)(y * (airData.Data[0][0].Count) / (yArrowCount));
                    int sampleZ = (int)(z * (airData.Data[0].Count) / (zArrowCount));

                    Vector3 pos = new Vector3(xCoord, yCoord, zCoord);
                    Vector3 dir = -new Vector3(
                        (float)airData.Data[sampleX][sampleZ][sampleY][1],
                        (float)airData.Data[sampleX][sampleZ][sampleY][2],
                        (float)airData.Data[sampleX][sampleZ][sampleY][3]
                    );
                    float scale = dir.magnitude;
                    DrawArrow(pos, dir, scale);
                }
            }
        }
    }

    private void DrawArrow(Vector3 pos, Vector3 dir, float magnitude)
    {
        float scale = (float)
            Math.Log(1.2 * Math.Min(0.99 * maxMagnitude * 1e2, magnitude * 1e2) + 1f);

        if (scale < 0.01f)
        {
            // early return for tiny air flow
            return;
        }

        GameObject newArrow = Instantiate(
            windArrowPrefab,
            pos,
            Quaternion.LookRotation(dir, Vector3.up),
            transform
        );
        newArrow.transform.localScale = new Vector3(scale, scale, scale * 4f);

        float relativeMagnitude = (magnitude - minMagnitude) / maxMagnitude;
        // Debug.Log("Relative magnitude: " + relativeMagnitude);
        Color color = windColorGradient.Evaluate(relativeMagnitude);
        newArrow.GetComponent<Renderer>().material.color = color;

        Renderer renderer = newArrow.GetComponent<Renderer>();
        renderer.material.color = color;
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        renderer.receiveShadows = false;

        newArrow.SetActive(active);
        windArrows.Add(newArrow);
    }
}
