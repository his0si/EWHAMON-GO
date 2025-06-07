using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public bool isBgmOn = true;
    public bool isSfxOn = true;

    void Awake()
    {
        // 싱글톤 패턴
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 넘어가도 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 저장된 설정 불러오기
        isBgmOn = PlayerPrefs.GetInt("BGM_ON", 1) == 1;
        isSfxOn = PlayerPrefs.GetInt("SFX_ON", 1) == 1;
        ApplySettings();
    }

    public void ToggleBGM()
    {
        isBgmOn = !isBgmOn;
        PlayerPrefs.SetInt("BGM_ON", isBgmOn ? 1 : 0);
        ApplySettings();
    }

    public void ToggleSFX()
    {
        isSfxOn = !isSfxOn;
        PlayerPrefs.SetInt("SFX_ON", isSfxOn ? 1 : 0);
        ApplySettings();
    }

    public void ApplySettings()
    {
        bgmSource.mute = !isBgmOn;
        sfxSource.mute = !isSfxOn;
    }

    public void PlaySFX(AudioClip clip)
    {
        if (isSfxOn)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
