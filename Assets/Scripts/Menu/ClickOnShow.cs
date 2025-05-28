using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickOnShowUI : MonoBehaviour
{
    public GameObject[] objectsToShow;
    private bool isShown = false;
    public Button myButton;

    void Start()
    {
        if (myButton != null)
        {
            myButton.onClick.AddListener(ToggleObjects);
        }
    }

    void ToggleObjects()
    {
        isShown = !isShown;
        foreach (GameObject obj in objectsToShow)
        {
            obj.SetActive(isShown);
        }
    }

}