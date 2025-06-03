using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;

public class CreateImagePrefab
{
    [MenuItem("Tools/Create Image Prefab")]
    public static void CreatePrefab()
    {
        GameObject go = new GameObject("ImageItem", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));

        // ✅ Image 컴포넌트 가져오기
        Image img = go.GetComponent<Image>();
        img.color = Color.white;
        img.raycastTarget = true;

        // ✅ 비율 무시하고 정사각형에 맞게 채우기
        img.preserveAspect = false;
        img.type = Image.Type.Simple;

        // ✅ RectTransform 설정
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(500, 500); // 셀 크기에 맞춤
        rt.anchorMin = new Vector2(0, 1);
        rt.anchorMax = new Vector2(0, 1);
        rt.pivot = new Vector2(0.5f, 0.5f);

        // ✅ 저장 경로 생성
        string folderPath = "Assets/Prefabs";
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        // ✅ 프리팹 저장
        string localPath = folderPath + "/ImageItem.prefab";
        PrefabUtility.SaveAsPrefabAsset(go, localPath);

        Debug.Log("✅ 프리팹 생성 완료: " + localPath);
        Object.DestroyImmediate(go);
    }
}
