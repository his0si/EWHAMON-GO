using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public List<MonsterData> allMonsters;
    public List<PlaceData> allPlaces;  // ⬅️ 추가됨
}

public class GameStateManager : MonoBehaviour
{
    public List<MonsterManager> monsterManagers;
    public List<PlaceManager> placeManagers;

    private string savePath;

    private void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "game_state.json");
    }

    public void SaveGame()
    {
        GameState state = new GameState();
        state.allMonsters = new List<MonsterData>();
        state.allPlaces = new List<PlaceData>();

        foreach (var manager in monsterManagers)
        {
            state.allMonsters.Add(manager.data);
        }

        foreach (var place in placeManagers)
        {
            PlaceData pdata = new PlaceData
            {
                placeName = place.placeName,
                level = place.level,
                isCaught = place.isCaught
            };
            state.allPlaces.Add(pdata);
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

        for (int i = 0; i < placeManagers.Count; i++)
        {
            var data = state.allPlaces[i];
            placeManagers[i].placeName = data.placeName;
            placeManagers[i].level = data.level;
            placeManagers[i].isCaught = data.isCaught;
        }

        Debug.Log("Game Loaded.");
    }
}
