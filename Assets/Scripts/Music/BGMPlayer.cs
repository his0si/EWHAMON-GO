using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMPlayer : MonoBehaviour
{
    public static BGMPlayer Instance;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.playOnAwake = true;

            // 저장된 설정 불러와서 적용
            bool bgmOn = PlayerPrefs.GetInt("bgm_on", 1) == 1;
            audioSource.mute = !bgmOn;
        }
        else
        {
            Destroy(gameObject); // 중복 제거
        }
    }

    public void SetBGMVolume(bool on)
    {
        audioSource.mute = !on;
    }
}
