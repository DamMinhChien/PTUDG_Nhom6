using UnityEngine;
using TMPro;

public class QuestCompletePopup : MonoBehaviour
{
    public static QuestCompletePopup Instance;

    public TextMeshProUGUI messageText;
    public GameObject popupObject;

    private void Awake()
    {
        Instance = this;
        popupObject.SetActive(false);
    }

    public void Show(string questTitle)
    {
        popupObject.SetActive(true);
        messageText.text = $"🎉 Đã hoàn thành nhiệm vụ:\n<b>{questTitle}</b>";

        CancelInvoke(nameof(Hide)); // nếu trước đó đang đếm ngược
        Invoke(nameof(Hide), 3f);    // tự động ẩn sau 3 giây
    }

    public void Hide()
    {
        popupObject.SetActive(false);
    }
}
