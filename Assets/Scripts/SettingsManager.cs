using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("BGM")]
    public Button bgmToggleButton;
    public Sprite bgmOnSprite;
    public Sprite bgmOffSprite;

    [Header("SFX")]
    public Button sfxToggleButton;
    public Sprite sfxOnSprite;
    public Sprite sfxOffSprite;

    void Start()
    {   
        UpdateBGMUI();
        UpdateSFXUI();
    }

    public void ToggleBGM()
    {
        bool bgmOn = PlayerPrefs.GetInt("bgm_on", 1) == 1;
        bool newValue = !bgmOn;
        PlayerPrefs.SetInt("bgm_on", newValue ? 1 : 0);
        PlayerPrefs.Save();

        // BGMPlayer 싱글톤에 전달
        if (BGMPlayer.Instance != null)
            BGMPlayer.Instance.SetBGMVolume(newValue);

        UpdateBGMUI();
    }

    public void ToggleSFX()
    {
        bool sfxOn = PlayerPrefs.GetInt("sfx", 1) == 1;
        bool newValue = !sfxOn;
        PlayerPrefs.SetInt("sfx", newValue ? 1 : 0);
        PlayerPrefs.Save();

        UpdateSFXUI();
    }

    void UpdateBGMUI()
    {
        bool bgmOn = PlayerPrefs.GetInt("bgm_on", 1) == 1;
        if (bgmToggleButton != null && bgmToggleButton.image != null)
            bgmToggleButton.image.sprite = bgmOn ? bgmOnSprite : bgmOffSprite;
    }

    void UpdateSFXUI()
    {
        bool sfxOn = PlayerPrefs.GetInt("sfx", 1) == 1;
        if (sfxToggleButton != null && sfxToggleButton.image != null)
            sfxToggleButton.image.sprite = sfxOn ? sfxOnSprite : sfxOffSprite;
    }
}
