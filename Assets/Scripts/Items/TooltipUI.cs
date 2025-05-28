using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public RectTransform backgroundRect;

    public static TooltipUI Instance;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent as RectTransform,
            Input.mousePosition,
            null,
            out pos);
        transform.localPosition = pos + new Vector2(10f, -10f);
    }

    public void Show(string name, string description)
    {
        nameText.text = name;
        descriptionText.text = description;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
