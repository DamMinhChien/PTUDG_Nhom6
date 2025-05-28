using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject itemListContainer;
    public GameObject itemEntryPrefab;

    private Dictionary<string, GameObject> itemEntries = new Dictionary<string, GameObject>();

    public static InventoryUI Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        inventoryPanel.SetActive(false);
        InventoryManager.Instance.OnInventoryChanged += UpdateUI;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            if (inventoryPanel.activeSelf)
                UpdateUI();
        }
    }

    public void UpdateUI()
    {
        Debug.Log($"Số lượng item trong inventory: {InventoryManager.Instance.GetAllItems().Count}");
        foreach (var entry in itemEntries.Values)
            Destroy(entry);
        itemEntries.Clear();

        foreach (var pair in InventoryManager.Instance.GetAllItems())
        {
            var item = pair.Value;
            GameObject newItem = Instantiate(itemEntryPrefab, itemListContainer.transform);
            var itemUI = newItem.GetComponent<InventoryItemUI>();
            itemUI.Setup(item.data.id, item.data.name, item.quantity, item.data.iconSprite, item.data.description);
            itemEntries[pair.Key] = newItem;
        }
    }

    /// Gọi hàm này từ InventoryManager khi có thay đổi
    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if (inventoryPanel.activeSelf)
            UpdateUI();
    }

}
