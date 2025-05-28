using UnityEngine;
using System.Collections;

public class InventoryDebugAdder : MonoBehaviour
{
    IEnumerator Start()
    {
        // Đợi InventoryManager khởi tạo
        while (InventoryManager.Instance == null || GameDataLoader.Instance == null)
        {
            yield return null;
        }

        InventoryManager.Instance.AddItem("item_pokeball", 3);
        InventoryManager.Instance.AddItem("item_hp", 1);
        InventoryManager.Instance.AddItem("item_kn", 21);
        InventoryManager.Instance.AddItem("tien_hoa_thuy", 1);
        InventoryManager.Instance.AddItem("tien_hoa_tho", 100);
        InventoryManager.Instance.AddItem("tien_hoa_hoa", 10);
        InventoryManager.Instance.AddItem("tien_hoa_moc", 11);

        var data = GameDataLoader.Instance.GetItemDataById("item_pokeball");
        if (data == null)
            Debug.LogError("❌ Không tìm thấy 'item_pokeball'");
        else
            Debug.Log("✅ Tìm thấy: " + data.name);
        Debug.Log("Tổng số item: " + InventoryManager.Instance.GetAllItems().Count);
    }
}