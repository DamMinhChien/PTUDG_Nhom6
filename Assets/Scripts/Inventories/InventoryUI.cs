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
        foreach (var entry in itemEntries.Values)
            Destroy(entry);
        itemEntries.Clear();

        foreach (var pair in InventoryManager.Instance.GetAllItems())
        {
            GameObject newItem = Instantiate(itemEntryPrefab, itemListContainer.transform);
            newItem.SetActive(true);
            newItem.GetComponentInChildren<TextMeshProUGUI>().text = $"{pair.Key} x{pair.Value}";
            itemEntries.Add(pair.Key, newItem);
        }
    }
}
