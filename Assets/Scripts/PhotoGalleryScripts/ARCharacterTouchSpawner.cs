using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class ARCharacterTouchSpawner : MonoBehaviour
{
    public GameObject character;

    private GameObject spawnedCharacter;
    private GraphicRaycaster uiRaycaster;
    private EventSystem eventSystem;

    void Start()
    {
        // 자동으로 컴포넌트 찾아서 연결
        uiRaycaster = FindObjectOfType<GraphicRaycaster>();
        eventSystem = EventSystem.current;

        if (uiRaycaster == null || eventSystem == null)
        {
            Debug.LogError("❌ GraphicRaycaster 또는 EventSystem이 연결되지 않았습니다.");
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUI(Input.mousePosition)) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                SpawnCharacter(hit.point);
            }
        }
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPos = Input.GetTouch(0).position;
            if (IsPointerOverUI(touchPos)) return;

            Ray ray = Camera.main.ScreenPointToRay(touchPos);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                SpawnCharacter(hit.point);
            }
        }
#endif
    }

    void SpawnCharacter(Vector3 position)
    {
        if (spawnedCharacter != null)
        {
            Destroy(spawnedCharacter);
        }

        spawnedCharacter = Instantiate(character, position, Quaternion.Euler(0, 100, 0));
        spawnedCharacter.transform.localScale = Vector3.one * 0.01f;
        Debug.Log("✅ 캐릭터 생성됨: " + position);
    }

    bool IsPointerOverUI(Vector2 screenPos)
    {
        if (uiRaycaster == null || eventSystem == null)
            return false;

        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = screenPos
        };

        List<RaycastResult> results = new List<RaycastResult>();
        uiRaycaster.Raycast(pointerData, results);

        return results.Count > 0;
    }
}
