using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryClickOpenerUI : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (InventoryUI.Instance != null)
        {
            InventoryUI.Instance.ToggleInventory();
        }
    }
}
