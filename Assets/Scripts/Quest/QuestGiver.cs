using UnityEngine;
using UnityEngine.EventSystems;

public class QuestGiver : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject questPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (questPanel != null)
        {
            questPanel.SetActive(!questPanel.activeSelf);
        }
    }
}
