using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SavedPokemon
{
    public string id;
    public int level;
    public int currentHP;
    public List<string> learnedSkillIds;
}

[System.Serializable]
public class SaveData
{
    public List<SavedPokemon> myPokemons;
    public int playerCoin;
    public int playerGold;
    public List<string> ownedItems;
}

public class SaveDataManager : MonoBehaviour
{
    public GameData gameData;
    public SaveData saveData;

    //StreamingAssets: cần tạo thư mục này để lưu những file tĩnh, Unity đảm bảo k bỏ qua khi build game
    private string gameDataPath => Path.Combine(Application.streamingAssetsPath, "game_data.json");
    //persistentDataPath: là nơi lưu dữ liệu lâu dài như level, Unity đảm bảo không bị mất khi thoát game
    private string saveDataPath => Path.Combine(Application.persistentDataPath, "save.json");

    void Start()
    {
        LoadGameData();

        if (File.Exists(saveDataPath))
        {
            LoadSaveData();
            Debug.Log("Save loaded.");
        }
        else
        {
            CreateInitialSaveData();
            SaveGame();
            Debug.Log("New save created.");
        }

        // In thử Pokémon đã lưu
        foreach (var p in saveData.myPokemons)
        {
            Debug.Log($"[SAVE] Pokémon: {p.id} - Level {p.level} - HP: {p.currentHP}");
        }
    }

    void LoadGameData()
    {
        if (File.Exists(gameDataPath))
        {
            string json = File.ReadAllText(gameDataPath);
            gameData = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            Debug.LogError("Game data file not found!");
        }
    }

    void LoadSaveData()
    {
        string json = File.ReadAllText(saveDataPath);
        saveData = JsonUtility.FromJson<SaveData>(json);
    }

    void SaveGame()
    {
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(saveDataPath, json);
    }

    void CreateInitialSaveData()
    {
        saveData = new SaveData
        {
            myPokemons = new List<SavedPokemon>(),
            ownedItems = new List<string>(),
            playerGold = 2,
            playerCoin = 1000
        };

        foreach (var p in gameData.pokemon)
        {
            saveData.myPokemons.Add(new SavedPokemon
            {
                id = p.id,
                level = p.level,
                currentHP = p.hp,
                learnedSkillIds = new List<string>() // Chưa học kỹ năng nào
            });
        }
    }
}
