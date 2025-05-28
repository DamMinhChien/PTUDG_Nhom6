using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class QuestUIManager : MonoBehaviour
{
    public static QuestUIManager Instance;

    public GameObject questPanel;
    public GameObject questEntryPrefab;
    public Transform questListContainer;
    //
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI progressText;
    //
    public TextMeshProUGUI descriptionText;
    public Button completeButton;

    private Quest currentQuest;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ShowQuests()
    {
        foreach (Transform child in questListContainer)
            Destroy(child.gameObject);

        foreach (var quest in QuestManager.Instance.quests)
        {
            GameObject entry = Instantiate(questEntryPrefab, questListContainer);

            // ✅ Gán dữ liệu nhiệm vụ vào prefab
            QuestEntryUI entryUI = entry.GetComponent<QuestEntryUI>();
            if (entryUI != null)
            {
                entryUI.Setup(quest);
            }
            else
            {
                Debug.LogError("❌ Prefab thiếu script QuestEntryUI!");
            }

            // ✅ Gắn sự kiện click vào entry
            entry.GetComponent<Button>().onClick.AddListener(() => ShowQuestDetail(quest));

            Debug.Log("🔹 Tạo entry cho: " + quest.title);
        }
    }

    public void ShowQuestDetail(Quest quest)
    {
        currentQuest = quest;
        descriptionText.text = quest.description;
        //
        titleText.text = quest.title;

        int current = InventoryManager.Instance.GetItemAmount(quest.requiredItem);
        //
        progressText.text = $"{current}/{quest.requiredAmount}" + (quest.isCompleted ? " ✅" : "");
        //
        completeButton.gameObject.SetActive(!quest.isCompleted && current >= quest.requiredAmount);
    }

    public void CompleteQuest()
    {
        if (currentQuest == null || currentQuest.isCompleted) return;

        InventoryManager.Instance.RemoveItem(currentQuest.requiredItem, currentQuest.requiredAmount);
        currentQuest.isCompleted = true;
        completeButton.gameObject.SetActive(false);

        Debug.Log($"🎉 Hoàn thành nhiệm vụ: {currentQuest.title}");
    }

    public void ToggleQuestPanel()
    {
        questPanel.SetActive(!questPanel.activeSelf);
        if (questPanel.activeSelf)
            ShowQuests();
    }
}
