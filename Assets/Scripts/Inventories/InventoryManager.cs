using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private Dictionary<string, Item> inventory = new Dictionary<string, Item>();

    public delegate void InventoryChanged();
    public event InventoryChanged OnInventoryChanged;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        
    }

    public Dictionary<string, Item> GetAllItems()
    {
        return new Dictionary<string, Item>(inventory);
    }

    public bool HasItem(string itemId, int requiredAmount)
    {
        return inventory.ContainsKey(itemId) && inventory[itemId].quantity >= requiredAmount;
    }

    public int GetItemAmount(string itemId)
    {
        if (inventory.ContainsKey(itemId))
            return inventory[itemId].quantity;
        return 0;
    }

    private void NotifyChange()
    {
        OnInventoryChanged?.Invoke();
    }

    public void AddItem(string itemId, int amount)
    {
        ItemData data = GameDataLoader.Instance.GetItemDataById(itemId);
        if (data == null)
        {
            Debug.LogWarning($"Không tìm thấy dữ liệu item: {itemId}");
            return;
        }

        if (!inventory.ContainsKey(itemId))
        {
            inventory[itemId] = new Item(data, amount);
        }
        else
        {
            inventory[itemId].quantity += amount;
        }

        Debug.Log($"✅ Nhận {amount} x {data.name}");
        QuestManager.Instance.CheckQuestProgress(itemId);
        NotifyChange();
    }

    public void RemoveItem(string itemId, int amount)
    {
        if (!inventory.ContainsKey(itemId)) return;

        inventory[itemId].quantity -= amount;
        if (inventory[itemId].quantity <= 0)
            inventory.Remove(itemId);

        NotifyChange();
    }

    public Item GetItem(string itemId)
    {
        if (inventory.TryGetValue(itemId, out Item item))
            return item;
        return null;
    }

}
