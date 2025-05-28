using UnityEngine;
using UnityEngine.UI;

public class ItemAction : MonoBehaviour
{
    public Button useButton;
    public Button dropButton;

    private string currentItemId;

    public static ItemAction Instance;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Show(string itemId, Vector2 screenPosition)
    {
        currentItemId = itemId;
        gameObject.SetActive(true);
        transform.position = screenPosition;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        useButton.onClick.AddListener(OnUseClicked);
        dropButton.onClick.AddListener(OnDropClicked);
    }

    private void OnUseClicked()
    {
        Debug.Log($"📦 Sử dụng item: {currentItemId}");
        // TODO: Thêm logic sử dụng item tại đây
        Hide();
    }

    private void OnDropClicked()
    {
        InventoryManager.Instance.RemoveItem(currentItemId, 1);
        Hide();
    }
}
