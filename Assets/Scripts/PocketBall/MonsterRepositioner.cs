using UnityEngine;
using Vuforia;

public class MonsterRepositioner : MonoBehaviour
{
    public GameObject monster;
    public BallThrower thrower;
    public GameObject btnThrowStart;
    public GameObject guidePanel;

    // ✅ 위치, 회전, 스케일 직접 지정 가능하게
    public Vector3 monsterPosition = new Vector3(0.5f, -0.5f, 4f);
    public Vector3 monsterRotation = new Vector3(0f, 100f, 0f);
    public Vector3 monsterScale = new Vector3(0.02f, 0.02f, 0.02f);

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

        if (monster == null || thrower == null || btnThrowStart == null) return;

        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
            behaviour.gameObject.SetActive(false);

            // ✅ 인스펙터 값으로 설정
            monster.transform.position = monsterPosition;
            monster.transform.rotation = Quaternion.Euler(monsterRotation);
            monster.transform.localScale = monsterScale;

            monster.SetActive(true);
            Debug.Log("✅ 몬스터 위치 및 회전 고정 완료");

            thrower.monster = monster;
            thrower.OnMarkerDetected();

            btnThrowStart.SetActive(true);
            Debug.Log("✅ 버튼 활성화 완료");

            if (guidePanel != null)
            {
                guidePanel.SetActive(true);
                Debug.Log("✅ 패널 표시 완료");
            }
        }
    }
}
