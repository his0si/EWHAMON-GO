using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MonsterDetailManager : MonoBehaviour
{
    public Image monsterImage;
    public TMP_Text nameText;
    public TMP_Text levelText;
    public TMP_Text descriptionText;

    public Sprite[] sprites_Lv1to3;  // LV1~LV3 몬스터 이미지
    public Sprite pokeballSprite;

    private string place;
    private int level;

    void Start()
    {

        place = PlayerPrefs.GetString("dex_place");
        level = PlayerPrefs.GetInt("dex_level");
        UpdateDisplay();
    }

    public void OnClickLeft()
{
    level--;
    if (level < 1) level = 3;
    UpdateDisplay();
}

public void OnClickRight()
{
    level++;
    if (level > 3) level = 1;
    UpdateDisplay();
}


    public void OnClickBack()
    {
        SceneManager.LoadScene("13_MonsterDex");
    }

    void UpdateDisplay()
    {
        bool isCaught = PlayerPrefs.GetInt($"caught_{place}_{level}", 0) == 1;

        monsterImage.sprite = isCaught ? sprites_Lv1to3[level - 1] : pokeballSprite;
        nameText.text = isCaught ? GetMonsterName(place, level) : "???";
        levelText.text = $"Lv.{level}";
        descriptionText.text = isCaught ? GetDescription(place, level) : "???";
    }

    string GetMonsterName(string place, int level)
    {
        if (place == "ECC")
        {
            if (level == 1) return "이큐다";
            if (level == 2) return "배플리아";
            if (level == 3) return "엑시클레버";
        }
        else if (place == "POS")
        {
            if (level == 1) return "포냥코";
            if (level == 2) return "포스캣";
            if (level == 3) return "포닉스";
        }
        else if (place == "ENG")
        {
            if (level == 1) return "코딩어려엉";
            if (level == 2) return "버그시러엉";
            if (level == 3) return "코딩마스터엉";
        }
        return "???";
    }

    string GetDescription(string place, int level)
{
    string species = place switch
    {
        "ECC" => "ECC몬",
        "POS" => "포스코몬",
        "ENG" => "공대몬",
        _ => "???"
    };

    string habitat = place switch
    {
        "ECC" => "ECC",
        "POS" => "포스코관",
        "ENG" => "공대",
        _ => "???"
    };

    if (place == "ECC")
    {
        if (level == 1)
            return $"이화몬 종: {species}\n주 서식 장소: {habitat}\n특징: 영화보는 것을 좋아한다.\nHint: ECC에 있는 영화관에 가면 좋은 일이 일어날 수도..";
        if (level == 2)
            return $"이화몬 종: {species}\n주 서식 장소: {habitat}\n특징: 학생증을 잃어버렸다.\nHint: B3층에서 마지막으로 사용한 기억이 있다.";
        if (level == 3)
            return $"이화몬 종: {species}\n주 서식 장소: {habitat}\n특징: 세상은 이화에게 물었고, 이화는 엑시클레버를 답했다.\nHint: -";
    }
    else if (place == "POS")
    {
        if (level == 1)
            return $"이화몬 종: {species}\n주 서식 장소: {habitat}\n특징: 매일 점심은 아리랑도시락을 먹는다.\nHint: 포스코관 1층에 먹을 곳이 많다.";
        if (level == 2)
            return $"이화몬 종: {species}\n주 서식 장소: {habitat}\n특징: 복수 전공을 위해 종과로 자주 간다.\nHint: 포스코관에서 종합과학관으로 갈 수 있는 가장 빠른 방법은 무엇일까?";
        if (level == 3)
            return $"이화몬 종: {species}\n주 서식 장소: {habitat}\n특징: 꽃잎처럼 우아하게, 그러나 누구보다 강하게 나아간다.\nHint: -";
    }
    else if (place == "ENG")
    {
        if (level == 1)
            return $"이화몬 종: {species}\n주 서식 장소: {habitat}\n특징: 공대도서관에서 공부를 자주 한다.\nHint: 공부하고 있는 코딩어려엉을 찾아가보자.";
        if (level == 2)
            return $"이화몬 종: {species}\n주 서식 장소: {habitat}\n특징: 버그 걸릴 때마다 아이스티노를 찾는다.\nHint: 공대식당 근처에 깔끔한 카페가 있던데…";
        if (level == 3)
            return $"이화몬 종: {species}\n주 서식 장소: {habitat}\n특징: 이화의 지성과 품격이 완성된 존재.수많은 밤을 공대에서 보내며 갈고닦은 실력은 이제 세상을 흔들 힘이 되었다.\nHint: -";
    }

    return "???";
}

public void OnClickCamera()
{
    // 현재 보고 있는 몬스터 정보 저장
    PlayerPrefs.SetString("ar_place", place);
    PlayerPrefs.SetInt("ar_level", level);
    PlayerPrefs.Save();

    // ✅ 수정된 씬 이름으로 이동
    SceneManager.LoadScene("15_Camera_Capture");
}



}
