using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialogueUI; // UI hiển thị hội thoại
    private bool playerInRange = false;
    public GameObject interactHint;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            interactHint.SetActive(playerInRange);

            if (playerInRange && Input.GetKeyDown(KeyCode.E))
            {
                dialogueUI.SetActive(!dialogueUI.activeSelf);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogueUI.SetActive(false); // Ẩn UI khi rời khỏi
        }
    }
}
