using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterStage
{
    public int stageNumber;
    public string name;
    public string appearance;
    public string requirement;
    public string oneLine;
    public string modelFile;
}

[Serializable]
public class Monster
{
    public string monsterName;
    public string campusZone;
    public string shortDescription;
    public string hint;
    public List<MonsterStage> stages;
}

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class MonsterDatabase : MonoBehaviour
{
    public List<Monster> monsters;

    void Awake()
    {
        monsters = new List<Monster>
        {
            new Monster
            {
                monsterName = "ECC",
                campusZone = "ECC",
                shortDescription = "애벌레에서 나비로 성장하는 몬스터 라인",
                hint = null,
                stages = new List<MonsterStage>
                {
                    new MonsterStage
                    {
                        stageNumber = 1,
                        name = "이큐다",
                        appearance = "애벌레 + 배꽃 머리핀",
                        requirement = "ECC 영화관에 가면 좋은 일이…?",
                        oneLine = "영화 보는 것을 좋아한다.",
                        modelFile = "ecc_1.fbx"
                    },
                    new MonsterStage
                    {
                        stageNumber = 2,
                        name = "배플리아",
                        appearance = "나비 + 배꽃 머리핀",
                        requirement = "학생증을 B3층에서 마지막으로 사용한 기억",
                        oneLine = "학생증을 잃어버렸다.",
                        modelFile = "ecc_2.fbx"
                    },
                    new MonsterStage
                    {
                        stageNumber = 3,
                        name = "엑시클레버",
                        appearance = "졸업 학위복 + 학사모",
                        requirement = null,
                        oneLine = "세상은 이화에게 물었고, 이화는 엑시클레버를 답했다.",
                        modelFile = "ecc_3.fbx"
                    }
                }
            },
            // PK, EN 등 다른 몬스터도 같은 방식으로 추가
        };
    }
}
