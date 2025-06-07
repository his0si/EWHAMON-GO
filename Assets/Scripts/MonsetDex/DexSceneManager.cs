using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DexSceneManager : MonoBehaviour
{
    [Header("카드 슬롯 3개 (ECC, POS, ENG 순서)")]
    public Image[] monsterImages;         // 몬스터 이미지
    public TMP_Text[] monsterNameTexts;   // 몬스터 이름 텍스트
    public TMP_Text[] monsterLevelTexts;  // 레벨 텍스트

    [Header("몬스터 이미지 (LV1~3 순서)")]
    public Sprite[] eccSprites;
    public Sprite[] posSprites;
    public Sprite[] engSprites;
    public Sprite pokeballSprite;

    void Start()
    {
        UpdateCard("ECC", 0, eccSprites);
        UpdateCard("POS", 1, posSprites);
        UpdateCard("ENG", 2, engSprites);
    }

    void UpdateCard(string place, int index, Sprite[] sprites)
    {
        int level = PlayerPrefs.GetInt($"monster_{place}_level", 0);
        int caught = PlayerPrefs.GetInt($"caught_{place}_1", 0); // Lv.1 잡았는지 기준

        if (caught == 0 || level == 0)
        {
            // 아직 안 잡았을 경우
            monsterImages[index].sprite = pokeballSprite;
            monsterNameTexts[index].text = "???";
            monsterLevelTexts[index].text = "";
        }
        else
        {
            monsterImages[index].sprite = sprites[level - 1];
            monsterNameTexts[index].text = GetMonsterName(place, level);
            monsterLevelTexts[index].text = $"Lv.{level}";
        }
    }

    public void OnClickECC()
    {
        Debug.Log("🟢 ECC 카드 클릭됨");
        GoToDetail("ECC");
    }

    public void OnClickPOS()
    {
        Debug.Log("🟢 POS 카드 클릭됨");
        GoToDetail("POS");
    }

    public void OnClickENG()
    {
        Debug.Log("🟢 ENG 카드 클릭됨");
        GoToDetail("ENG");
    }

    void GoToDetail(string place)
    {
        int level = PlayerPrefs.GetInt($"monster_{place}_level", 0);
        int finalLevel = level >= 1 ? level : 1;

        PlayerPrefs.SetString("dex_place", place);
        PlayerPrefs.SetInt("dex_level", finalLevel);
        SceneManager.LoadScene($"14_{place}_Monster_Detail");
    }

    string GetMonsterName(string place, int level)
    {
        if (place == "ECC")
        {
            if (level == 1) return "이큐다";
            if (level == 2) return "플로라";
            if (level == 3) return "블루밍";
        }
        else if (place == "POS")
        {
            if (level == 1) return "포스코몬";
            if (level == 2) return "캣츠포";
            if (level == 3) return "티탄포";
        }
        else if (place == "ENG")
        {
            if (level == 1) return "공대몬";
            if (level == 2) return "렌치부엉";
            if (level == 3) return "엔지빔";
        }
        return "???";
    }
}
