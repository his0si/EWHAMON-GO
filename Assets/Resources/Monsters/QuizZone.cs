using UnityEngine;

public class QuizZone : MonoBehaviour
{
    public MonsterManager monster;

    // 가정: 정답 맞춘 경우 이 함수 호출됨
    public void OnCorrectAnswer()
    {
        if (monster != null && monster.data.isCaught)
        {
            monster.Evolve();
            monster.CompleteMission();
        }
    }
}
