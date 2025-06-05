using UnityEngine;

public class BallThrower : MonoBehaviour
{
    public GameObject pokeballPrefab;
    public Transform throwPoint;
    public GameObject monster;
    public GameObject btnThrowStart;
    public int maxThrows = 3;

    private int throwCount = 0;
    public bool isCaught = false;

    private Vector2 startTouchPos, endTouchPos;
    private bool isDragging = false;

    public GameObject currentBall;
    private bool isMarkerDetected = false;

    void Update()
    {
        if (!isMarkerDetected || isCaught || throwCount >= maxThrows || currentBall == null)
            return;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPos = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            endTouchPos = Input.mousePosition;
            isDragging = false;
            ThrowBall();
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTouchPos = touch.position;
                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Ended && isDragging)
            {
                endTouchPos = touch.position;
                isDragging = false;
                ThrowBall();
            }
        }
#endif
    }

    public void OnMarkerDetected()
    {
        isMarkerDetected = true;
        Debug.Log("📌 마커 인식됨");
    }

    public void OnClickThrowStart()
    {
        if (btnThrowStart != null) btnThrowStart.SetActive(false);

        if (!isCaught && throwCount < maxThrows)
        {
            PrepareNextBall();
        }
    }

    void PrepareNextBall()
    {
        if (currentBall != null) Destroy(currentBall);

        Vector3 spawnPos = Camera.main.transform.position + Camera.main.transform.forward * 1.2f + Vector3.down * 0.2f;
        currentBall = Instantiate(pokeballPrefab, spawnPos, Quaternion.identity);
        currentBall.transform.localScale = new Vector3(20f, 20f, 20f);

        Rigidbody rb = currentBall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            rb.isKinematic = true;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }

        Pocketball pb = currentBall.GetComponent<Pocketball>();
        if (pb != null)
        {
            pb.monster = monster;
            pb.thrower = this;
        }

        currentBall.SetActive(true);
        Debug.Log("✅ 포켓볼 생성 완료!");
    }

    void ThrowBall()
    {
        if (currentBall == null)
        {
            Debug.LogWarning("⛔ currentBall이 null입니다.");
            return;
        }

        if (monster == null)
        {
            Debug.LogWarning("❗ monster가 비어 있음");
            return;
        }

        Rigidbody rb = currentBall.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("⛔ Rigidbody 없음");
            return;
        }

        Vector2 swipe = endTouchPos - startTouchPos;
        if (swipe.magnitude < 30f)
        {
            Debug.Log("⚠️ 드래그 너무 짧음");
            return;
        }

        float swipeX = swipe.x;
        Vector3 right = Camera.main.transform.right;
        float lateralOffset = Mathf.Clamp(swipeX * 0.01f, -2f, 2f);

        Vector3 adjustedTarget = monster.transform.position + right * lateralOffset;
        adjustedTarget.y = monster.transform.position.y + 0.3f;

        Vector3 direction = (adjustedTarget - currentBall.transform.position).normalized + Vector3.up * 1.2f;

        rb.isKinematic = false;  // ✅ 여기 중요!
        rb.AddForce(direction.normalized * 6f, ForceMode.Impulse);
        Debug.Log($"🟢 던지는 방향: {direction}, isKinematic 상태: {rb.isKinematic}");

        currentBall = null;
        throwCount++;

        Invoke(nameof(CheckHitResult), 2f);
    }

    void CheckHitResult()
    {
        if (!isCaught && throwCount < maxThrows)
        {
            PrepareNextBall();
        }
    }
}
