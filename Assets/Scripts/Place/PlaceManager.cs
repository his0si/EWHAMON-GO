using UnityEngine;
using TMPro;

public class PlaceManager : MonoBehaviour
{
    public string placeName;
    public int level = 1;
    public bool isCaught = false;

    public GameObject popupUI;
    public TextMeshProUGUI popupText;

    private void Start()
    {
        if (popupUI != null) popupUI.SetActive(false);
        UpdatePopupText();
    }

    public void OnClickPlace()
    {
        if (isCaught)
        {
            level++;
            isCaught = false;
        }

        UpdatePopupText();

        if (popupUI != null)
            popupUI.SetActive(true);
    }

    public void UpdatePopupText()
{
    if (popupText != null)
    {
        popupText.text = $"Lv.{level}";
    }
}

    public void ClosePopup()
    {
    if (popupUI != null)
        popupUI.SetActive(false);
      }


    public void MarkAsCaught()
    {
        isCaught = true;
    }

    // 외부에서 씬 이동을 호출할 때 사용
    public void GoToCameraScene()
    {
        PlayerPrefs.SetString("SelectedPlace", placeName);
        PlayerPrefs.SetInt("SelectedLevel", level);

        string sceneName = $"4_{placeName}_Camera";
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
