using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel; // Panel hiển thị hội thoại
    public TextMeshProUGUI dialogueText; // Text hiển thị nội dung
    public string[] dialogueLines; // Các dòng thoại
    private int currentLine = 0;

    public float typingSpeed = 0.03f;
    private bool isTyping = false;

    void Start()
    {
        dialoguePanel.SetActive(false); // Ẩn lúc đầu
    }

    public void StartDialogue()
    {
        currentLine = 0;
        dialoguePanel.SetActive(true);
        StartCoroutine(TypeLine());
    }

    void Update()
    {
        if (dialoguePanel.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[currentLine];
                isTyping = false;
            }
            else
            {
                currentLine++;
                if (currentLine < dialogueLines.Length)
                {
                    StartCoroutine(TypeLine());
                }
                else
                {
                    dialoguePanel.SetActive(false);
                }
            }
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }
}
