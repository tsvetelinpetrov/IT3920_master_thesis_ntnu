using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AirflowManager : MonoBehaviour
{
    public Mesh arrowMesh;
    public Material arrowMaterial;
    public Gradient windColorGradient;
    public int xArrowCount = 10;
    public int yArrowCount = 10;
    public int zArrowCount = 10;
    public bool active = true;

    private const int MAX_BATCH_SIZE = 1023;
    private List<Matrix4x4> matrices = new();
    private List<MaterialPropertyBlock> propertyBlocks = new();

    private float minMagnitude,
        maxMagnitude;
    private float minTemperature,
        maxTemperature;

    void OnEnable()
    {
        EventCenter.Airflow.OnNewAirflowData += OnNewAirflowData;
    }

    void OnDisable()
    {
        EventCenter.Airflow.OnNewAirflowData -= OnNewAirflowData;
    }

    void OnNewAirflowData(Airflow airflow)
    {
        // Clear previous data
        matrices.Clear();
        propertyBlocks.Clear();
        CalculateMagnitudeLimits(airflow);
        GenerateInstances(airflow);
        //transform.rotation = Quaternion.Euler(-90f, 90f, 0f);
    }

    void CalculateMagnitudeLimits(Airflow airData)
    {
        minMagnitude = float.MaxValue;
        maxMagnitude = float.MinValue;
        minTemperature = float.MaxValue;
        maxTemperature = float.MinValue;

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
                    float mag = dir.magnitude;
                    minMagnitude = Mathf.Min(minMagnitude, mag);
                    maxMagnitude = Mathf.Max(maxMagnitude, mag);

                    float temp = (float)airData.Data[x][y][z][0];
                    minTemperature = Mathf.Min(minTemperature, temp);
                    maxTemperature = Mathf.Max(maxTemperature, temp);
                }
            }
        }
    }

    void GenerateInstances(Airflow airData)
    {
        matrices.Clear();
        propertyBlocks.Clear();

        Vector3 origin = transform.position;
        Vector3 size = transform.localScale;

        float x0 = origin.x - size.x / 2f;
        float y0 = origin.y + size.y / 2f;
        float z0 = origin.z - size.z / 2f;
        float x1 = origin.x + size.x / 2f;
        float y1 = origin.y - size.y / 2f;
        float z1 = origin.z + size.z / 2f;

        float xStep = (x1 - x0) / xArrowCount;
        float yStep = (y1 - y0) / yArrowCount;
        float zStep = (z1 - z0) / zArrowCount;

        int xSize = airData.Data.Count;
        int ySize = airData.Data[0].Count;
        int zSize = airData.Data[0][0].Count;

        for (int x = 0; x < xArrowCount; x++)
        {
            float xCoord = x0 + x * xStep;
            int sampleX = x * xSize / xArrowCount;

            for (int y = 0; y < yArrowCount; y++)
            {
                float yCoord = y0 + y * yStep;
                int sampleY = y * ySize / yArrowCount;

                for (int z = 0; z < zArrowCount; z++)
                {
                    float zCoord = z0 + z * zStep;
                    int sampleZ = z * zSize / zArrowCount;

                    Vector3 pos = new(xCoord, yCoord, zCoord);
                    Vector3 dir = -new Vector3(
                        (float)airData.Data[sampleX][sampleY][sampleZ][1],
                        (float)airData.Data[sampleX][sampleY][sampleZ][2],
                        (float)airData.Data[sampleX][sampleY][sampleZ][3]
                    );

                    float magnitude = dir.magnitude;
                    if (magnitude < 0.01f)
                        continue;

                    float temperature = (float)airData.Data[sampleX][sampleY][sampleZ][0];

                    float scale = (float)
                        Math.Log(1.2 * Math.Min(0.99 * maxMagnitude * 1e2, magnitude * 1e2) + 1f);
                    Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
                    Matrix4x4 matrix = Matrix4x4.TRS(
                        pos,
                        rotation,
                        new Vector3(scale, scale, scale * 4f)
                    );
                    matrices.Add(matrix);

                    float relativeTemperature =
                        (temperature - minTemperature) / (maxTemperature - minTemperature);
                    Color color = windColorGradient.Evaluate(relativeTemperature);

                    MaterialPropertyBlock block = new MaterialPropertyBlock();
                    block.SetColor("_Color", color);
                    propertyBlocks.Add(block);
                }
            }
        }
    }

    void Update()
    {
        if (!active || matrices.Count == 0 || arrowMesh == null || arrowMaterial == null)
            return;

        for (int i = 0; i < matrices.Count; i += MAX_BATCH_SIZE)
        {
            int count = Mathf.Min(MAX_BATCH_SIZE, matrices.Count - i);
            var matrixSlice = matrices.GetRange(i, count);
            var blockSlice = propertyBlocks.GetRange(i, count);

            for (int j = 0; j < count; j++)
            {
                Graphics.DrawMesh(
                    arrowMesh,
                    matrixSlice[j],
                    arrowMaterial,
                    0,
                    null,
                    0,
                    blockSlice[j],
                    ShadowCastingMode.Off,
                    false
                );
            }
        }
    }
}
