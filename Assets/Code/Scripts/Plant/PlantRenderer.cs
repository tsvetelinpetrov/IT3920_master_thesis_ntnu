using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PlantRenderer
{
    public void renderPlant(string plantObjData)
    {
        GameObject plant = ParseObjString(plantObjData);

        plant.transform.position = new Vector3(0, 0.61f, 0);
        plant.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        // Material PlantMaterial = new Material(Shader.Find("Standard"));
        // PlantMaterial.color = Color.green;
        // PlantMaterial.SetFloat("_Glossiness", 0.5f);
        // plant.GetComponent<MeshRenderer>().material = PlantMaterial;
    }

    private GameObject ParseObjString(string objData)
    {
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> triangles = new List<int>();

        string[] lines = objData.Split('\n');

        foreach (string line in lines)
        {
            string[] parts = line.Split(' ');
            if (parts.Length < 2)
                continue;

            switch (parts[0])
            {
                case "v": // Vertex
                    vertices.Add(
                        new Vector3(
                            float.Parse(parts[1], CultureInfo.InvariantCulture),
                            float.Parse(parts[2], CultureInfo.InvariantCulture),
                            float.Parse(parts[3], CultureInfo.InvariantCulture)
                        )
                    );
                    break;

                case "vn": // Normal
                    normals.Add(
                        new Vector3(
                            float.Parse(parts[1], CultureInfo.InvariantCulture),
                            float.Parse(parts[2], CultureInfo.InvariantCulture),
                            float.Parse(parts[3], CultureInfo.InvariantCulture)
                        )
                    );
                    break;

                case "vt": // Texture Coordinate
                    uvs.Add(
                        new Vector2(
                            float.Parse(parts[1], CultureInfo.InvariantCulture),
                            float.Parse(parts[2], CultureInfo.InvariantCulture)
                        )
                    );
                    break;

                case "f": // Face
                    for (int i = 1; i < parts.Length; i++)
                    {
                        string[] faceData = parts[i].Split('/');
                        int vertexIndex = int.Parse(faceData[0]) - 1;
                        triangles.Add(vertexIndex);
                    }
                    break;
            }
        }

        // Create a new mesh
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        // Optional: Add normals and UVs if available
        if (normals.Count == vertices.Count)
            mesh.normals = normals.ToArray();
        if (uvs.Count > 0)
            mesh.uv = uvs.ToArray();

        // Recalculate bounds and normals if they weren't specified
        if (mesh.normals.Length == 0)
            mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        // Create the GameObject and assign the mesh
        GameObject obj = new GameObject("ImportedObj");
        MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();
        meshFilter.mesh = mesh;

        // Assign a default material (or set your own)
        meshRenderer.material = new Material(Shader.Find("Standard"));

        return obj;
    }
}
