using TMPro;
using UnityEngine;

public class QuestEntryUI : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI progressText;

    public Quest quest;

    private void OnEnable()
    {
        InventoryManager.Instance.OnInventoryChanged += UpdateProgress;
    }

    private void OnDisable()
    {
        if (InventoryManager.Instance != null)
            InventoryManager.Instance.OnInventoryChanged -= UpdateProgress;
    }

    public void Setup(Quest data)
    {
        quest = data;
        Debug.Log($"🧩 Setup item UI: {quest.title}");
        UpdateProgress();
    }

    public void UpdateProgress()
    {
        int current = InventoryManager.Instance.GetItemAmount(quest.requiredItem);
        progressText.text = $"{current}/{quest.requiredAmount}" + (quest.isCompleted ? " ✅" : "");
        titleText.text = quest.title;
    }
}
