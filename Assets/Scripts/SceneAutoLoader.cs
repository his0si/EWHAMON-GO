using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAutoLoader : MonoBehaviour
{
    public string nextSceneName = "18_0_OnBoarding";
    public float delayTime = 3f;

    void Start()
    {
        Debug.Log($"🕒 {delayTime}초 후 {nextSceneName} 씬으로 이동 예정");
        StartCoroutine(LoadSceneAfterDelay());
    }

    System.Collections.IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        Debug.Log("➡️ 씬 이동 시도");
        SceneManager.LoadScene(nextSceneName);
    }
}
