using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlaceManager : MonoBehaviour
{
    public string placeName; // 예: "ECC", "POS", "ENG"
    public int caughtLevel = 0;
    public GameObject popupUI;
    public TextMeshProUGUI popupText;
    public TextMeshProUGUI collectionRateText;
    public int level = 1;
    public bool isCaught = false;

    private string[] placeNames = { "ECC", "POS", "ENG" };
    private int totalMonsters = 9;

    void Start()
    {
        caughtLevel = PlayerPrefs.GetInt($"monster_{placeName}_level", 0);
        if (popupUI != null) popupUI.SetActive(false);
        UpdatePopupText();
        UpdateCollectionRate();
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
            for (int lvl = 1; lvl <= 3; lvl++)
            {
                if (PlayerPrefs.GetInt($"caught_{name}_{lvl}", 0) == 1)
                {
                    totalCaught++;
                }
            }
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

        // ✅ caught_{place}_{level} 저장
        PlayerPrefs.SetInt($"caught_{placeName}_{caughtLevel}", 1);

        PlayerPrefs.Save();

        Debug.Log($"✅ {placeName}의 몬스터를 Lv.{caughtLevel}로 저장했습니다");

        UpdatePopupText();
        UpdateCollectionRate();
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
        SceneManager.LoadScene(sceneName);
    }
}
