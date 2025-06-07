using UnityEngine;

public class BGMTester : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        Debug.Log("▶️ 강제 재생 시도");
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
