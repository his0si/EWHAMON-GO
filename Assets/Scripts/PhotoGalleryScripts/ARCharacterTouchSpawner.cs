using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class ARCharacterTouchSpawner : MonoBehaviour
{
    [System.Serializable]
    public class MonsterPrefab
    {
        public string key; // 예: "ECC_1"
        public GameObject prefab;
        public Vector3 scale = Vector3.one * 0.01f;
        public Vector3 rotation = new Vector3(0, 100, 0);
    }

    public MonsterPrefab[] monsterPrefabs;

    private GameObject spawnedCharacter;
    private GraphicRaycaster uiRaycaster;
    private EventSystem eventSystem;

    private GameObject selectedPrefab;
    private Vector3 selectedScale;
    private Quaternion selectedRotation;

    void Start()
    {
        uiRaycaster = FindObjectOfType<GraphicRaycaster>();
        eventSystem = EventSystem.current;

        string place = PlayerPrefs.GetString("ar_place", "ECC");
        int level = PlayerPrefs.GetInt("ar_level", 1);
        string key = $"{place}_{level}";

        // 🧩 프리팹 매칭
        foreach (var mp in monsterPrefabs)
        {
            if (mp.key == key)
            {
                selectedPrefab = mp.prefab;
                selectedScale = mp.scale;
                selectedRotation = Quaternion.Euler(mp.rotation);
                break;
            }
        }

        if (selectedPrefab == null)
        {
            Debug.LogError("❌ 프리팹을 찾을 수 없습니다: " + key);
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

        if (selectedPrefab == null) return;

        spawnedCharacter = Instantiate(selectedPrefab, position, selectedRotation);
        spawnedCharacter.transform.localScale = selectedScale;
        Debug.Log($"✅ 캐릭터 생성됨: {PlayerPrefs.GetString("ar_place")}_{PlayerPrefs.GetInt("ar_level")} at {position}");
    }

    bool IsPointerOverUI(Vector2 screenPos)
    {
        if (uiRaycaster == null || eventSystem == null)
            return false;

        PointerEventData pointerData = new PointerEventData(eventSystem) { position = screenPos };
        List<RaycastResult> results = new List<RaycastResult>();
        uiRaycaster.Raycast(pointerData, results);
        return results.Count > 0;
    }
}
