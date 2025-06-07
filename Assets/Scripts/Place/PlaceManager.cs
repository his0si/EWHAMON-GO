using UnityEngine;
using TMPro;

public class PlaceManager : MonoBehaviour
{
    public string placeName; // 예: "ECC"
    public int caughtLevel = 0;
    public GameObject popupUI;
    public TextMeshProUGUI popupText;
    public TextMeshProUGUI collectionRateText; // ← ✅ 추가: 수집률 텍스트
    public int level = 1;
    public bool isCaught = false;

    private string[] placeNames = { "ECC", "ENG", "POS" }; // ← ✅ 3종류의 장소명
    private int totalMonsters = 9;

    private void Start()
    {
        caughtLevel = PlayerPrefs.GetInt($"monster_{placeName}_level", 0);
        if (popupUI != null) popupUI.SetActive(false);
        UpdatePopupText();
        UpdateCollectionRate(); // ← ✅ 수집률 표시
    }

    public void OnClickPlace()
    {
        UpdatePopupText();
        if (popupUI != null) popupUI.SetActive(true);
    }

    public void UpdatePopupText()
    {
        if (popupText != null)
        {
            if (caughtLevel >= 3)
            {
                popupText.text = $"완료!";
            }
            else
            {
                int nextLevel = caughtLevel + 1;
                popupText.text = $"Lv.{nextLevel}";
            }
        }
    }

    public void UpdateCollectionRate()
    {
        if (collectionRateText == null) return;

        int totalCaught = 0;

        foreach (string name in placeNames)
        {
            int level = PlayerPrefs.GetInt($"monster_{name}_level", 0);
            totalCaught += Mathf.Clamp(level, 0, 3); // 최대 3마리로 제한
        }

        collectionRateText.text = $"도감 수집률: {totalCaught}/{totalMonsters}";
    }

    public void ClosePopup()
    {
        if (popupUI != null) popupUI.SetActive(false);
    }

    public void MarkAsCaught()
    {
        caughtLevel++;
        PlayerPrefs.SetInt($"monster_{placeName}_level", caughtLevel);
        Debug.Log($"✅ {placeName}의 몬스터를 Lv.{caughtLevel}로 저장했습니다");

        UpdatePopupText();       // 텍스트 갱신
        UpdateCollectionRate();  // 수집률 갱신
    }

    public void GoToCameraScene()
    {
        if (caughtLevel >= 3)
        {
            Debug.Log("✅ 모든 레벨 완료. 카메라 씬으로 이동하지 않음");
            return;
        }

        int nextLevel = caughtLevel + 1;
        PlayerPrefs.SetString("quiz_place", placeName);
        PlayerPrefs.SetInt("quiz_level", nextLevel);
        PlayerPrefs.Save();

        string sceneName = $"4_{placeName}_Camera_{nextLevel}";
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
