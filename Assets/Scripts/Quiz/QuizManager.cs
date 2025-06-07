using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    [Header("문제/정답 관련")]
    public TMP_Text questionText;
    public Button[] answerButtons;

    [Header("결과 패널")]
    public GameObject Right_Panel;
    public GameObject Wrong_Panel;

    [Header("정답 텍스트")]
    public TMP_Text rightHeaderMainText;
    public TMP_Text rightHeaderSubText;
    public TMP_Text rightContentText;

    [Header("오답 텍스트")]
    public TMP_Text wrongContentText;

    [Header("장소별 로고 이미지 (퀴즈 상단 UI용)")]
    public Image placeImage;
    public Sprite eccSprite;
    public Sprite posSprite;
    public Sprite engSprite;

    [Header("몬스터 이미지 (정답/오답 패널용)")]
    public Image rightMonsterImage;
    public Image wrongMonsterImage;
    public Sprite[] monsterSprites; // ECC: 0~2, POS: 3~5, ENG: 6~8

    private string place;
    private int level;

    void Start()
    {
        place = PlayerPrefs.GetString("quiz_place");
        level = PlayerPrefs.GetInt("quiz_level");

        Debug.Log($"🧩 퀴즈 로드됨: {place}, Lv.{level}");

        SetPlaceImage(place);
        LoadQuestion(place, level);
    }

    void SetPlaceImage(string place)
    {
        if (placeImage == null) return;

        switch (place)
        {
            case "ECC": placeImage.sprite = eccSprite; break;
            case "POS": placeImage.sprite = posSprite; break;
            case "ENG": placeImage.sprite = engSprite; break;
            default: placeImage.enabled = false; break;
        }
    }

    void SetMonsterImage(Image target)
    {
        if (monsterSprites == null || monsterSprites.Length < 9 || target == null) return;

        int offset = place switch {
            "ECC" => 0,
            "POS" => 3,
            "ENG" => 6,
            _ => 0
        };
        int index = offset + Mathf.Clamp(level - 1, 0, 2);
        target.sprite = monsterSprites[index];
    }

    public void OnClickRetry()
    {
        Wrong_Panel.SetActive(false);
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
        else if (place == "POS")
        {
            if (level == 1)
            {
                questionText.text = "포스코관의 공부 스팟이 아닌 곳은?";
                answerButtons[0].GetComponentInChildren<TMP_Text>().text = "1층 라운지";
                answerButtons[1].GetComponentInChildren<TMP_Text>().text = "6층 열람실";
                answerButtons[2].GetComponentInChildren<TMP_Text>().text = "지하 2층";

                string info = "포스코관의 공부 스팟은 1층 라운지, 6층 열람실, 지하1층이다.";
                answerButtons[0].onClick.AddListener(() => OnWrongAnswer("1층 라운지는 공부 스팟 맞아!"));
                answerButtons[1].onClick.AddListener(() => OnWrongAnswer("6층도 열람실 있어!"));
                answerButtons[2].onClick.AddListener(() => OnCorrectAnswer(info));
            }
            else if (level == 2)
            {
                questionText.text = "포관 1층에 있는 식당은?";
                answerButtons[0].GetComponentInChildren<TMP_Text>().text = "오봉도시락";
                answerButtons[1].GetComponentInChildren<TMP_Text>().text = "인생도시락";
                answerButtons[2].GetComponentInChildren<TMP_Text>().text = "이화도시락";

                string info = "오봉도시락은 포스코관 1층에 있는 식당으로, 아리랑 도시락이 맛있기로 유명하다.";
                answerButtons[0].onClick.AddListener(() => OnCorrectAnswer(info));
                answerButtons[1].onClick.AddListener(() => OnWrongAnswer("인생도시락은 없어!"));
                answerButtons[2].onClick.AddListener(() => OnWrongAnswer("이화도시락은 없어!"));
            }
            else if (level == 3)
            {
                questionText.text = "종합과학관과 연결되는 통로가 있는 층은?";
                answerButtons[0].GetComponentInChildren<TMP_Text>().text = "1층";
                answerButtons[1].GetComponentInChildren<TMP_Text>().text = "지하 2층";
                answerButtons[2].GetComponentInChildren<TMP_Text>().text = "4층";

                string info = "종합과학관과 연결된 층은 4층이다.";
                answerButtons[0].onClick.AddListener(() => OnWrongAnswer("1층은 아니야!"));
                answerButtons[1].onClick.AddListener(() => OnWrongAnswer("지하 2층은 연결 안 돼!"));
                answerButtons[2].onClick.AddListener(() => OnCorrectAnswer(info));
            }
        }
        else if (place == "ENG")
        {
            if (level == 1)
            {
                questionText.text = "공대에서 대여 가능한 기자재가 아닌 것은?";
                answerButtons[0].GetComponentInChildren<TMP_Text>().text = "드론";
                answerButtons[1].GetComponentInChildren<TMP_Text>().text = "VR기기";
                answerButtons[2].GetComponentInChildren<TMP_Text>().text = "마우스와 키보드";

                string info = "아산공학관 123호에서 기자재를 대여할 수 있다. 대여 가능한 기자재는 노트북, 태블릿, 스마트폰, 드론, 보드류, VR기기, AV케이블이 있다.";
                answerButtons[0].onClick.AddListener(() => OnWrongAnswer("드론은 대여 가능해!"));
                answerButtons[1].onClick.AddListener(() => OnWrongAnswer("VR기기도 대여돼!"));
                answerButtons[2].onClick.AddListener(() => OnCorrectAnswer(info));
            }
            else if (level == 2)
            {
                questionText.text = "공대 도서관에 대한 설명으로 옳지 않은 것은?";
                answerButtons[0].GetComponentInChildren<TMP_Text>().text = "24시간 개방이다.";
                answerButtons[1].GetComponentInChildren<TMP_Text>().text = "노트북 타자 사용은 노트북실이나 모둠학습실에서만 가능하다.";
                answerButtons[2].GetComponentInChildren<TMP_Text>().text = "모둠학습실은 예약 후 이용 가능하다.";

                string info = "모둠학습실은 예약 없이 선착순으로 이용 가능하다.";
                answerButtons[0].onClick.AddListener(() => OnWrongAnswer("24시간 개방 맞아!"));
                answerButtons[1].onClick.AddListener(() => OnWrongAnswer("그건 맞아!"));
                answerButtons[2].onClick.AddListener(() => OnCorrectAnswer(info));
            }
            else if (level == 3)
            {
                questionText.text = "공대 이화상점은 ?? 공학관 ??층에 있다.";
                answerButtons[0].GetComponentInChildren<TMP_Text>().text = "신공학관, 지하1층";
                answerButtons[1].GetComponentInChildren<TMP_Text>().text = "신공학관, 지하2층";
                answerButtons[2].GetComponentInChildren<TMP_Text>().text = "아산공학관, 지하2층";

                string info = "신공학관 지하 2층에 이화상점이 있다. 이화상점의 아이스티노는 이화의 자랑이다.";
                answerButtons[0].onClick.AddListener(() => OnWrongAnswer("지하 1층은 아니야!"));
                answerButtons[1].onClick.AddListener(() => OnCorrectAnswer(info));
                answerButtons[2].onClick.AddListener(() => OnWrongAnswer("아산공학관은 아니야!"));
            }
        }
    }

    void OnCorrectAnswer(string info)
    {
        Right_Panel.SetActive(true);
        SetMonsterImage(rightMonsterImage);

        rightHeaderMainText.text = "정답이에요!";
        rightHeaderSubText.text = level == 1 ? "이화몬을 포획했어요!" : $"이화몬이 Lv.{level}로 진화했어요!";
        rightContentText.text = info;

        Debug.Log("✅ 정답 패널 표시 완료");
    }

    void OnWrongAnswer(string hint)
    {
        Wrong_Panel.SetActive(true);
        SetMonsterImage(wrongMonsterImage);

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

        SceneManager.LoadScene("13_MonsterDex");
    }
}
