using UnityEngine;

public class MonsterLoader : MonoBehaviour
{
    public string monsterName;
    public GameObject monsterPrefab;
    public Transform monsterSpawnPoint;
    public BallThrower thrower;
    public MonsterRepositioner repositioner;

    void Start()
    {
        GameObject monster = Instantiate(monsterPrefab);
        monster.name = monsterPrefab.name;

        if (monsterSpawnPoint != null)
        {
            monster.transform.position = monsterSpawnPoint.position;
            monster.transform.rotation = monsterSpawnPoint.rotation;
        }

        monster.SetActive(false);

        if (thrower != null) thrower.monster = monster;
        if (repositioner != null) repositioner.monster = monster;

        Debug.Log("✅ 몬스터 로드 및 연결 완료");
    }
}
