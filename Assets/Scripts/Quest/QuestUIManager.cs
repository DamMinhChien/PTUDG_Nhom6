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

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI progressText;
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
            entry.GetComponentInChildren<TextMeshProUGUI>().text = quest.title;
            entry.GetComponent<Button>().onClick.AddListener(() => ShowQuestDetail(quest));
        }
    }

    public void ShowQuestDetail(Quest quest)
    {
        currentQuest = quest;
        titleText.text = quest.title;
        descriptionText.text = quest.description;

        int current = InventoryManager.Instance.GetItemAmount(quest.requiredItem);
        progressText.text = $"Tiến độ: {current}/{quest.requiredAmount}";

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
