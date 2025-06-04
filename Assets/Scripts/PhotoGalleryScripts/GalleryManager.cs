using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class GalleryManager : MonoBehaviour
{
    public Transform contentParent;           // Content 객체 (ScrollView 하위)
    public GameObject imageItemPrefab;        // Image + Button 프리팹
    public GameObject previewPanel;           // 화면 덮는 Preview 패널
    public Image previewImage;                // 확대해서 보여줄 이미지

    private string imageFolderPath;

    void Start()
    {
        imageFolderPath = Application.persistentDataPath;
        LoadImages();
    }

    void LoadImages()
    {
        string[] files = Directory.GetFiles(imageFolderPath, "photo_*.png");
        foreach (string file in files)
        {
            // 텍스처 로드
            byte[] imageData = File.ReadAllBytes(file);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData);

            Sprite sprite = Sprite.Create(texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));

            // 프리팹으로 아이템 생성
            GameObject item = Instantiate(imageItemPrefab, contentParent);
            Image img = item.GetComponent<Image>();
            img.sprite = sprite;
            img.preserveAspect = false;

            // 정사각형 크기로 강제 고정
            RectTransform rt = item.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(500, 500);

            // 클릭 시 previewImage에만 sprite 설정
            Button btn = item.GetComponent<Button>();
            btn.onClick.AddListener(() => ShowPreview(sprite));
        }
    }

    void ShowPreview(Sprite sprite)
    {
        previewImage.sprite = sprite;
        previewPanel.SetActive(true);
    }

    public void HidePreview()
    {
        previewPanel.SetActive(false);
        previewImage.sprite = null;
    }
}
