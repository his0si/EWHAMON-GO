// Pocketball.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pocketball : MonoBehaviour
{
    public GameObject monster;
    public BallThrower thrower;

    private string placeName;
    private int monsterLevel;

    private void Start()
    {
        placeName = PlayerPrefs.GetString("quiz_place", "UnknownPlace");
        monsterLevel = PlayerPrefs.GetInt("quiz_level", 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"💥 충돌 감지됨! {gameObject.name} 이(가) {collision.gameObject.name} 와 충돌함");

        if (thrower == null || monster == null)
        {
            Debug.LogWarning("⚠️ thrower 또는 monster가 null입니다");
            return;
        }

        if (thrower.isCaught) return;

        if (collision.transform.IsChildOf(monster.transform))
        {
            Debug.Log("✅ 몬스터 잡힘!");
            thrower.isCaught = true;
            monster.SetActive(false);
            Destroy(gameObject);

            PlayerPrefs.SetString("quiz_place", placeName);
            PlayerPrefs.SetInt("quiz_level", monsterLevel);
            PlayerPrefs.Save();

            SceneManager.LoadScene("8_Quiz");
        }
        else
        {
            Debug.Log("❌ 몬스터 아님! 맞은 건: " + collision.transform.name);
        }
    }
}
