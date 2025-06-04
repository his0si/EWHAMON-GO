using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class ARPhotoManager : MonoBehaviour
{
    public Button captureButton;

    void Start()
    {
        captureButton.onClick.AddListener(() => StartCoroutine(CaptureAndSave()));
    }

    IEnumerator CaptureAndSave()
    {
        yield return new WaitForEndOfFrame();

        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        string filename = "photo_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string path = Path.Combine(Application.persistentDataPath, filename);
        File.WriteAllBytes(path, screenshot.EncodeToPNG());

        Debug.Log("✅ Saved to: " + path);
    }
}
