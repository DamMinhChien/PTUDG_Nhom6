using UnityEngine;
using System.Collections;

public class InventoryDebugAdder : MonoBehaviour
{
    IEnumerator Start()
    {
        // Đợi tất cả hệ thống cần thiết được khởi tạo
        while (
            InventoryManager.Instance == null ||
            GameDataLoader.Instance == null ||
            QuestLoader.Instance == null ||
            QuestUIManager.Instance == null
        )
        {
            yield return null;
        }

        // Thêm item mẫu
        InventoryManager.Instance.AddItem("item_pokeball", 3);
        InventoryManager.Instance.AddItem("item_hp", 1);
        InventoryManager.Instance.AddItem("item_kn", 21);
        InventoryManager.Instance.AddItem("tien_hoa_thuy", 1);
        InventoryManager.Instance.AddItem("tien_hoa_tho", 100);
        InventoryManager.Instance.AddItem("tien_hoa_hoa", 10);
        InventoryManager.Instance.AddItem("tien_hoa_moc", 11);

        Debug.Log("🧾 Tổng số item: " + InventoryManager.Instance.GetAllItems().Count);

        // Lấy nhiệm vụ từ JSON đã load
        Quest quest = QuestLoader.Instance.GetQuestById("quest_001");
        if (quest != null)
        {
            QuestUIManager.Instance.ShowQuestDetail(quest);
            Debug.Log("✅ Đã hiển thị nhiệm vụ: " + quest.title);
        }
        else
        {
            Debug.LogWarning("⚠ Không tìm thấy nhiệm vụ quest_001!");
        }
    }

}