using UnityEngine;

public class ScenePositioner : MonoBehaviour
{
    public Transform throwPoint;
    public GameObject monster;
    public GameObject backgroundImage;

    public float ballDistance = 1.0f;
    public float monsterDistance = 2.5f;
    public float backgroundDistance = 4.0f;
    public float monsterScale = 0.2f;

    public void PlaceObjectsInOrder()
    {
        if (Camera.main == null || throwPoint == null || monster == null || backgroundImage == null)
        {
            Debug.LogError("❌ 필수 오브젝트가 연결되지 않았습니다.");
            return;
        }

        Transform cam = Camera.main.transform;
        Vector3 forward = cam.forward;

        // 1. 포켓볼 위치
        throwPoint.position = cam.position + forward * ballDistance;

        // 2. 몬스터 위치
        monster.transform.position = cam.position + forward * monsterDistance;
        monster.transform.rotation = Quaternion.LookRotation(-forward);
        monster.transform.localScale = Vector3.one * monsterScale;

        // 3. 배경 위치
        backgroundImage.transform.position = cam.position + forward * backgroundDistance;
        backgroundImage.transform.rotation = Quaternion.LookRotation(-forward);

        Debug.Log("📌 포켓볼 → 몬스터 → 배경 순서로 배치 완료");
    }
}
