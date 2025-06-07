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
      PlayerPrefs.DeleteKey("caught_ENG_1");
       PlayerPrefs.DeleteKey("caught_ENG_2");
       PlayerPrefs.DeleteKey("caught_ENG_3");
       PlayerPrefs.SetInt("monster_ENG_level", 0);
       PlayerPrefs.Save();
      
    }

    void UpdateCard(string place, int index, Sprite[] sprites)
  {
      int level = PlayerPrefs.GetInt($"monster_{place}_level", 0);
      bool isCaught = level >= 1;

      // 이미지 설정
      monsterImages[index].sprite = isCaught ? sprites[level - 1] : pokeballSprite;

      // 이름 설정
      monsterNameTexts[index].text = isCaught ? GetMonsterName(place, level) : "???";

      // ✅ 레벨 텍스트 설정: 안 잡았으면 표시 X
      monsterLevelTexts[index].text = isCaught ? $"Lv.{level}" : "";
  }


    // 인스펙터에서 버튼 OnClick에 직접 연결할 함수들
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

    // 몬스터 이름 하드코딩
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
