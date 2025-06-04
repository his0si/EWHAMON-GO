using UnityEngine;

public class Pocketball : MonoBehaviour
{
    public GameObject monster;
    public BallThrower thrower;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"💥 충돌 발생: {collision.gameObject.name}");

        if (thrower == null || monster == null)
        {
            Debug.LogWarning("❗ thrower 또는 monster 참조가 비어 있음");
            return;
        }

        if (thrower.isCaught) return;

        // 핵심: 자식 오브젝트 콜라이더에도 대응
        Transform hitRoot = collision.transform.root;
        Transform monsterRoot = monster.transform;

        Debug.Log($"🎯 비교: 충돌={hitRoot.name}, 몬스터={monsterRoot.name}");

        if (hitRoot == monsterRoot)
        {
            Debug.Log("✅ 몬스터 잡힘!");
            thrower.isCaught = true;
            monster.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("❌ 몬스터가 아님");
        }
    }
}
