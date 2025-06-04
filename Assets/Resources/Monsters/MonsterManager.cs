using UnityEngine;

[System.Serializable]
public class MonsterData
{
    public string name;
    public int evolutionStage; // 0=외관, 1단계, 2단계, 3단계
    public bool isCaught;
    public bool missionCompleted;
}

public class MonsterManager : MonoBehaviour
{
    public MonsterData data;

    public void CatchMonster()
    {
        data.isCaught = true;
    }

    public void CompleteMission()
    {
        data.missionCompleted = true;
    }

    public void Evolve()
    {
        if (data.evolutionStage < 3)
        {
            data.evolutionStage++;
            Debug.Log($"{data.name} has evolved to stage {data.evolutionStage}");
        }
    }

    public string GetStageName()
    {
        switch (data.evolutionStage)
        {
            case 0: return "외관";
            case 1: return "1단계";
            case 2: return "2단계";
            case 3: return "3단계";
            default: return "알 수 없음";
        }
    }
}
