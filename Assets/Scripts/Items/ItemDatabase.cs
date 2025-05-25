using UnityEngine;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;
    private Dictionary<string, ItemData> items = new Dictionary<string, ItemData>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        LoadItemsFromJSON();
    }

    private void LoadItemsFromJSON()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("items"); // đường dẫn: Resources/Data/items.json
        if (jsonFile == null)
        {
            Debug.LogError("❌ Không tìm thấy file items.json trong Resources/Data/");
            return;
        }

        ItemData[] itemArray = JsonHelper.FromJson<ItemData>(jsonFile.text);
        foreach (var item in itemArray)
        {
            // Tự động load icon từ thư mục Resources/Icons/
            Sprite sprite = Resources.Load<Sprite>(/*"Icons/" + */item.icon);
            if (sprite == null)
                Debug.LogWarning($"⚠ Không tìm thấy icon: {item.icon}");
            else
                Debug.Log($"✅ Đã load icon: {item.icon}");

            item.icon = null; // giữ lại tên hoặc dùng sprite riêng nếu cần

            items[item.id] = item;
        }

        Debug.Log($"Đã load {items.Count} item từ JSON.");
    }

    public ItemData GetItemData(string id)
    {
        return items.TryGetValue(id, out var item) ? item : null;
    }
}
