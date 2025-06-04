using UnityEngine;
using Vuforia;

public class MonsterRepositioner : MonoBehaviour
{
    public GameObject monster;
    public BallThrower thrower;
    public GameObject btnThrowStart;

    void Start()
    {
        var observer = GetComponent<ObserverBehaviour>();
        if (observer != null)
        {
            observer.OnTargetStatusChanged += OnStatusChanged;
            Debug.Log("📡 마커 감지 리스너 등록됨");
        }
        else
        {
            Debug.LogError("❌ ObserverBehaviour가 없음 (마커에 붙어 있어야 함)");
        }
    }

    void OnStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        Debug.Log($"📡 마커 상태 변경됨: {status.Status}");

        if (monster == null || thrower == null || btnThrowStart == null)
        {
            Debug.LogError("❌ Monster, Thrower 또는 Button 연결 안됨");
            return;
        }

        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
            // 마커 비활성화
            behaviour.gameObject.SetActive(false);

            // ✅ 몬스터 위치/회전 고정
            monster.transform.position = new Vector3(0.5f, -0.5f, 4f);
            monster.transform.rotation = Quaternion.Euler(0f, 100f, 0f);
            monster.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);

            monster.SetActive(true);
            Debug.Log("✅ 몬스터 위치 및 회전 고정 완료");

            thrower.monster = monster;
            thrower.OnMarkerDetected(); // ✅ BallThrower에 마커 감지 전달

            btnThrowStart.SetActive(true);
            Debug.Log("✅ 버튼 활성화 완료");
        }
    }
}
