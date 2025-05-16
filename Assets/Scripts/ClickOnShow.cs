using UnityEngine;

public class ClickOnShow : MonoBehaviour
{
    public GameObject[] objectsToShow;
    private bool isShown = false;
    void OnMouseDown()
    {
        isShown = !isShown;
        foreach (GameObject obj in objectsToShow)
        {
            obj.SetActive(isShown);
        }
    }
}
