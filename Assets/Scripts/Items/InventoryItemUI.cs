using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryItemUI : MonoBehaviour, IPointerClickHandler/*IPointerEnterHandler, IPointerExitHandler*/
{
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI quantityText;

    private string itemName;
    private string itemDescription;
    public string itemId;

    public void Setup(string itemId, string itemName, int quantity, Sprite iconSprite, string description)
    {
        Debug.Log($"Setup item UI: {itemName}, x{quantity}");
        this.itemId = itemId;

        this.itemName = itemName;
        this.itemDescription = description;

        nameText.text = itemName;
        quantityText.text = "x" + quantity;
        iconImage.sprite = iconSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (TooltipUI.Instance == null)
            return;

        if (TooltipUI.Instance.gameObject.activeSelf)
        {
            TooltipUI.Instance.Hide();
        }
        else
        {
            TooltipUI.Instance.Show(itemName, itemDescription);
        }
        Vector2 screenPos = Input.mousePosition;

        ItemAction.Instance.Show(itemId, screenPos);
    }
}
