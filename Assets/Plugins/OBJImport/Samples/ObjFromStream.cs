using System.IO;
using System.Text;
using Dummiesman;
using UnityEngine;

public class ObjFromStream : MonoBehaviour
{
    public Material plantMaterial;

    void Start()
    {
        //make www
        var www = new WWW("http://192.168.0.100/greenhouse/mesh/low_res");
        while (!www.isDone)
            System.Threading.Thread.Sleep(1);

        //create stream and load
        var textStream = new MemoryStream(Encoding.UTF8.GetBytes(www.text));
        var loadedObj = new OBJLoader().Load(textStream);

        // Apply material
        foreach (var mesh in loadedObj.GetComponentsInChildren<MeshRenderer>())
        {
            mesh.material = plantMaterial;
        }
    }
}
