using System.Collections.Generic;
using UnityEngine;

public class QuestLoader : MonoBehaviour
{
    public static QuestLoader Instance;

    private Dictionary<string, Quest> questDict = new Dictionary<string, Quest>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        LoadQuests();
    }

    private void LoadQuests()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("quests"); // Đường dẫn: Resources/Data/quests.json
        if (jsonFile == null)
        {
            Debug.LogError("Không tìm thấy file quests.json trong Resources/Data!");
            return;
        }

        Quest[] quests = JsonHelper.FromJson<Quest>(jsonFile.text);

        foreach (var quest in quests)
        {
            questDict[quest.questId] = quest;
        }

        Debug.Log($"Đã load {questDict.Count} nhiệm vụ từ JSON.");
    }

    public Quest GetQuestById(string questId)
    {
        if (questDict.TryGetValue(questId, out Quest quest))
            return quest;
        return null;
    }

    public Dictionary<string, Quest> GetAllQuests()
    {
        return questDict;
    }

}
