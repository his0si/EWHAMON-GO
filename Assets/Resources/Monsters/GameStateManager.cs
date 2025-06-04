using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public List<MonsterData> allMonsters;
}

public class GameStateManager : MonoBehaviour
{
    public List<MonsterManager> monsterManagers;

    private string savePath;

    private void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "game_state.json");
    }

    public void SaveGame()
    {
        GameState state = new GameState();
        state.allMonsters = new List<MonsterData>();

        foreach (var manager in monsterManagers)
        {
            state.allMonsters.Add(manager.data);
        }

        string json = JsonUtility.ToJson(state, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game Saved.");
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("No save file found!");
            return;
        }

        string json = File.ReadAllText(savePath);
        GameState state = JsonUtility.FromJson<GameState>(json);

        for (int i = 0; i < monsterManagers.Count; i++)
        {
            monsterManagers[i].data = state.allMonsters[i];
        }

        Debug.Log("Game Loaded.");
    }
}
