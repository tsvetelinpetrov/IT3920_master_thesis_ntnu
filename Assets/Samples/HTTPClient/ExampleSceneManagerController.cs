using System.IO;
using CI.HttpClient;
using UnityEngine;
using UnityEngine.UI;

public class ExampleSceneManagerController : MonoBehaviour
{
    public Text LeftText;
    public Text RightText;
    public Slider ProgressSlider;
    public string Url;

    public void Upload()
    {
        HttpClient client = new HttpClient();

        byte[] buffer = new byte[1000000];
        new System.Random().NextBytes(buffer);

        ByteArrayContent content = new ByteArrayContent(buffer, "application/bytes");

        ProgressSlider.value = 0;

        client.Post(
            new System.Uri(Url),
            content,
            HttpCompletionOption.AllResponseContent,
            (r) => { },
            (u) =>
            {
                LeftText.text = "Upload: " + u.PercentageComplete.ToString() + "%";
                ProgressSlider.value = u.PercentageComplete;
            }
        );
    }

    public void Download()
    {
        HttpClient client = new HttpClient();

        ProgressSlider.value = 100;

        MemoryStream memoryStream = new MemoryStream();

        client.Get(
            new System.Uri(Url),
            HttpCompletionOption.StreamResponseContent,
            (r) =>
            {
                RightText.text = "Download: " + r.PercentageComplete.ToString() + "%";
                ProgressSlider.value = 100 - r.PercentageComplete;

                // When a chunk of data is received
                if (r.HasContent)
                {
                    memoryStream.Write(r.ReadAsByteArray(), 0, r.ReadAsByteArray().Length);
                    Debug.Log("Downloaded " + memoryStream.Length + " bytesss");
                }

                // When the download is complete
                if (r.IsSuccessStatusCode && r.PercentageComplete == 100)
                {
                    // Get the full downloaded content as a byte array
                    byte[] downloadedData = memoryStream.ToArray();
                    // Process the data or display it as needed
                    RightText.text =
                        "Download Complete. Downloaded KB : " + downloadedData.Length / 1024;
                }
            }
        );
    }

    public void UploadDownload()
    {
        HttpClient client = new HttpClient();

        byte[] buffer = new byte[1000000];
        new System.Random().NextBytes(buffer);

        ByteArrayContent content = new ByteArrayContent(buffer, "application/bytes");

        ProgressSlider.value = 0;

        client.Post(
            new System.Uri(Url),
            content,
            HttpCompletionOption.StreamResponseContent,
            (r) =>
            {
                RightText.text = "Download: " + r.PercentageComplete.ToString() + "%";
                ProgressSlider.value = 100 - r.PercentageComplete;
            },
            (u) =>
            {
                LeftText.text = "Upload: " + u.PercentageComplete.ToString() + "%";
                ProgressSlider.value = u.PercentageComplete;
            }
        );
    }

    public void Delete()
    {
        HttpClient client = new HttpClient();
        client.Delete(
            new System.Uri(Url),
            HttpCompletionOption.AllResponseContent,
            (r) =>
            {
#pragma warning disable 0219
                string responseData = r.ReadAsString();
#pragma warning restore 0219
            }
        );
    }

    public void Get()
    {
        HttpClient client = new HttpClient();
        client.Get(
            new System.Uri(Url),
            HttpCompletionOption.AllResponseContent,
            (r) =>
            {
#pragma warning disable 0219
                byte[] responseData = r.ReadAsByteArray();
#pragma warning restore 0219
            }
        );
    }

    public void Patch()
    {
        HttpClient client = new HttpClient();

        StringContent content = new StringContent("Hello World");

        client.Patch(
            new System.Uri(Url),
            content,
            HttpCompletionOption.AllResponseContent,
            (r) =>
            {
#pragma warning disable 0219
                Stream responseData = r.ReadAsStream();
#pragma warning restore 0219
            }
        );
    }

    public void Post()
    {
        HttpClient client = new HttpClient();

        StringContent content = new StringContent("Hello World");

        client.Post(
            new System.Uri(Url),
            content,
            HttpCompletionOption.AllResponseContent,
            (r) =>
            {
#pragma warning disable 0219
                string responseData = r.ReadAsString();
#pragma warning restore 0219
            }
        );
    }

    public void Put()
    {
        HttpClient client = new HttpClient();

        StringContent content = new StringContent("Hello World");

        client.Put(
            new System.Uri(Url),
            content,
            HttpCompletionOption.AllResponseContent,
            (r) =>
            {
#pragma warning disable 0219
                string responseData = r.ReadAsString();
#pragma warning restore 0219
            }
        );
    }
}
