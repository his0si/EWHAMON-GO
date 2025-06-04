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

    private GameObject currentBall;
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

        Vector3 spawnPos = new Vector3(0f, -0.4f, 1f);
        currentBall = Instantiate(pokeballPrefab, spawnPos, Quaternion.identity);
        currentBall.transform.localScale = new Vector3(20f, 20f, 20f);

        Rigidbody rb = currentBall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            rb.isKinematic = true;
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
        if (currentBall == null) return;

        Rigidbody rb = currentBall.GetComponent<Rigidbody>();
        if (rb == null) return;

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

        rb.isKinematic = false;
        rb.AddForce(direction.normalized * 6f, ForceMode.Impulse);
        Debug.Log($"🟢 던지는 방향: {direction}");

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
