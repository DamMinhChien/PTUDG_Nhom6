using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public List<Quest> quests = new List<Quest>();
    private Dictionary<string, Quest> activeQuests = new Dictionary<string, Quest>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        Debug.Log("📦 QuestManager.Start() chạy");

        var allQuests = QuestLoader.Instance?.GetAllQuests();
        if (allQuests == null)
        {
            Debug.LogWarning("⚠ QuestLoader.Instance.GetAllQuests() == null");
            return;
        }

        Debug.Log("📄 Tổng số nhiệm vụ từ JSON: " + allQuests.Count);

        foreach (var questPair in allQuests)
        {
            quests.Add(questPair.Value);
            Debug.Log("✅ Thêm nhiệm vụ: " + questPair.Value.title);
        }
    }


    public void LoadQuestsFromJSON(TextAsset questJSON)
    {
        Quest[] questArray = JsonHelper.FromJson<Quest>(questJSON.text);
        quests = new List<Quest>(questArray);
        Debug.Log($"Đã load {quests.Count} nhiệm vụ từ JSON");
    }

    public Quest GetQuestById(string id)
    {
        return quests.Find(q => q.questId == id);
    }
    public void AddQuest(Quest quest)
    {
        if (quest == null || activeQuests.ContainsKey(quest.questId))
            return;

        activeQuests[quest.questId] = quest;
        Debug.Log($"Đã thêm nhiệm vụ: {quest.title}");
    }

    public void CompleteQuest(string id)
    {
        Quest quest = GetQuestById(id);
        if (quest != null && !quest.isCompleted)
        {
            quest.isCompleted = true;
            Debug.Log("🎯 Đã hoàn thành nhiệm vụ: " + quest.title);

            QuestCompletePopup.Instance?.Show(quest.title);

            // TODO: Thưởng vàng, vật phẩm
        }
    }

    public bool HasQuest(string questId)
    {
        bool exists = activeQuests.ContainsKey(questId);
        Debug.Log($"🔍 Kiểm tra nhiệm vụ {questId}: {(exists ? "Đã có" : "Chưa có")}");
        return exists;
    }

    public void CheckQuestProgress(string itemId)
    {
        foreach (var quest in activeQuests.Values)
        {
            if (!quest.isCompleted && quest.requiredItem == itemId)
            {
                int currentAmount = InventoryManager.Instance.GetItemAmount(itemId);
                if (currentAmount >= quest.requiredAmount)
                {
                    CompleteQuest(quest.questId);
                }
            }
        }
    }
}
