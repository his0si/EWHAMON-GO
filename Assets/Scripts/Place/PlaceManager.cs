using UnityEngine;
using TMPro;

public class PlaceManager : MonoBehaviour
{
    public string placeName; // 예: "ECC"
    public int caughtLevel = 0;
    public GameObject popupUI;
    public TextMeshProUGUI popupText;
    public int level = 1;
    public bool isCaught = false;


    private void Start()
    {
        caughtLevel = PlayerPrefs.GetInt($"monster_{placeName}_level", 0);
        if (popupUI != null) popupUI.SetActive(false);
        UpdatePopupText();
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
            int nextLevel = caughtLevel + 1;
            popupText.text = $"Lv.{nextLevel}";
        }
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
    }

    public void GoToCameraScene()
    {
        int nextLevel = caughtLevel + 1;
        PlayerPrefs.SetString("quiz_place", placeName);
        PlayerPrefs.SetInt("quiz_level", nextLevel);
        PlayerPrefs.Save();

        string sceneName = $"4_{placeName}_Camera_{nextLevel}";
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
