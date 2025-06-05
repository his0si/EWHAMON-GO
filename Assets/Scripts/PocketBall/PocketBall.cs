using UnityEngine;
using UnityEngine.SceneManagement;

public class Pocketball : MonoBehaviour
{
    public GameObject monster;
    public BallThrower thrower;

    // 장소 및 레벨 (몬스터에 따라 설정되어야 함)
    private string placeName;
    private int monsterLevel;

    private void Start()
    {
        // 자동으로 현재 장소와 레벨 가져오기
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

        // 자식 포함 비교
        if (collision.transform.IsChildOf(monster.transform))
        {
            Debug.Log("✅ 몬스터 잡힘!");
            thrower.isCaught = true;
            monster.SetActive(false);
            Destroy(gameObject);

            // 👉 잡힌 몬스터 정보 저장
            PlayerPrefs.SetString("quiz_place", placeName);
            PlayerPrefs.SetInt("quiz_level", monsterLevel);
            PlayerPrefs.Save();

            // 👉 퀴즈 씬으로 이동
            SceneManager.LoadScene("8_Quiz");
        }
        else
        {
            Debug.Log("❌ 몬스터 아님! 맞은 건: " + collision.transform.name);
        }
    }
}
