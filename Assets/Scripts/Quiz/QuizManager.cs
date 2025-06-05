using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public TMP_Text questionText;
    public Button[] answerButtons;

    public GameObject Right_Panel;
    public GameObject Wrong_Panel;

    public TMP_Text rightHeaderMainText;
    public TMP_Text rightHeaderSubText;
    public TMP_Text rightContentText;

    public TMP_Text wrongContentText;

    private string place;
    private int level;

    void Start()
{
    place = PlayerPrefs.GetString("quiz_place");
    level = PlayerPrefs.GetInt("quiz_level");

    Debug.Log($"🧩 퀴즈 로드됨: {place}, Lv.{level}");  // 이게 꼭 2가 돼야 해
    LoadQuestion(place, level);
}



    void LoadQuestion(string place, int level)
  {
      Debug.Log($"🧩 퀴즈 로딩 시작 - 장소: {place}, 레벨: {level}");

      if (place == "ECC")
      {
          if (level == 1)
          {
              questionText.text = "외부인이 출입 가능한 층은 몇 층인가요?";
              answerButtons[0].GetComponentInChildren<TMP_Text>().text = "B1층";
              answerButtons[1].GetComponentInChildren<TMP_Text>().text = "B3층";
              answerButtons[2].GetComponentInChildren<TMP_Text>().text = "B4층";

              string info = "ECC 지하 4층만 외부인 출입이 가능하다. 그 외의 층은 재학생 및 교직원 전용 공간이다.";
              answerButtons[0].onClick.AddListener(() => OnWrongAnswer("B1층은 출입 불가야!"));
              answerButtons[1].onClick.AddListener(() => OnWrongAnswer("B3층은 내부인만 출입 가능해."));
              answerButtons[2].onClick.AddListener(() => OnCorrectAnswer(info));
          }
          else if (level == 2)
          {
              questionText.text = "ECC에 있는 영화관의 이름은?";
              answerButtons[0].GetComponentInChildren<TMP_Text>().text = "아트하우스 모모";
              answerButtons[1].GetComponentInChildren<TMP_Text>().text = "아트하우스 호호";
              answerButtons[2].GetComponentInChildren<TMP_Text>().text = "아트하우스 포포";

              string info = "ECC 영화관은 아트하우스 모모로, B402호이다. 현장예매시 이화재학생은 1000원 할인 혜택이 있다.";
              answerButtons[0].onClick.AddListener(() => OnCorrectAnswer(info));
              answerButtons[1].onClick.AddListener(() => OnWrongAnswer("호호는 아니고 모모야!"));
              answerButtons[2].onClick.AddListener(() => OnWrongAnswer("포포는 아냐!"));
          }
          else if (level == 3)
          {
              questionText.text = "ECC에 있는 학생 서비스 센터에 대해 옳지 않은 것은?";
              answerButtons[0].GetComponentInChildren<TMP_Text>().text = "학생증 발급이 가능하다";
              answerButtons[1].GetComponentInChildren<TMP_Text>().text = "분실물 습득 시, 접수가 가능하다";
              answerButtons[2].GetComponentInChildren<TMP_Text>().text = "주말에도 이용이 가능하다";

              string info = "학생서비스 센터는 평일만 운영해! 주말,공휴일은 휴무야. 위치는 B303호고, 분실물 접수도 가능해.";
              answerButtons[0].onClick.AddListener(() => OnWrongAnswer("이건 맞는 설명이야!"));
              answerButtons[1].onClick.AddListener(() => OnWrongAnswer("분실물도 처리 가능해!"));
              answerButtons[2].onClick.AddListener(() => OnCorrectAnswer(info));
          }
      }
  }



    void OnCorrectAnswer(string info)
    {
        Right_Panel.SetActive(true);

        if (level == 1)
        {
            rightHeaderMainText.text = "정답이에요!";
            rightHeaderSubText.text = "이화몬을 포획했어요!";
        }
        else
        {
            rightHeaderMainText.text = "정답이에요!";
            rightHeaderSubText.text = $"이화몬이 Lv.{level}로 진화했어요!";
        }

        rightContentText.text = info;

        Debug.Log("✅ 정답 패널 표시 완료");
    }

    void OnWrongAnswer(string hint)
    {
        Wrong_Panel.SetActive(true);
        wrongContentText.text = hint;

        Debug.Log("❌ 오답 패널 표시 완료");
    }

    public void OnClickAddToDex()
{
    string key = $"caught_{place}_{level}";
    if (!PlayerPrefs.HasKey(key))
    {
        PlayerPrefs.SetInt(key, 1);
        Debug.Log($"✅ 도감에 추가 완료: {key}, 진화 레벨 저장됨: monster_{place}_level={level}");
    }
    else
    {
        Debug.Log($"⚠️ 이미 등록된 몬스터입니다: {key}");
    }

    PlayerPrefs.SetInt($"monster_{place}_level", level);
    PlayerPrefs.Save();

    // 👉 도감 씬으로 이동
    UnityEngine.SceneManagement.SceneManager.LoadScene("13_MonsterDex");
}

}
