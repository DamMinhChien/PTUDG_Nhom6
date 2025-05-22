using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private Dictionary<string, int> inventory = new Dictionary<string, int>();
    
    public delegate void InventoryChanged();
    public event InventoryChanged OnInventoryChanged;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
        
    public Dictionary<string, int> GetAllItems()
    {
        return new Dictionary<string, int>(inventory);
    }

    public bool HasItem(string itemId, int requiredAmount)
    {
        return inventory.ContainsKey(itemId) && inventory[itemId] >= requiredAmount;
    }

    public int GetItemAmount(string itemId)
    {
        if (inventory.ContainsKey(itemId))
            return inventory[itemId];
        return 0;
    }
    private void NotifyChange()
    {
        OnInventoryChanged?.Invoke();
    }

    public void AddItem(string itemId, int amount)
    {
        if (inventory.ContainsKey(itemId))
            inventory[itemId] += amount;
        else
            inventory[itemId] = amount;

        Debug.Log($"Đã nhận {amount} x {itemId}");
        NotifyChange();
    }

    public void RemoveItem(string itemId, int amount)
    {
        if (!inventory.ContainsKey(itemId)) return;

        inventory[itemId] -= amount;
        if (inventory[itemId] <= 0)
            inventory.Remove(itemId);

        NotifyChange();
    }

}
