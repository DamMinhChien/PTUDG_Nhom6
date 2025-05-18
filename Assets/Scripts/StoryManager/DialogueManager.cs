using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;

    private Queue<string> dialogueLines;
    private bool isDialogueActive = false;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    [SerializeField] private float typingSpeed = 0.03f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        dialogueUI.SetActive(false);
        dialogueLines = new Queue<string>();
    }

    private void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                // Nếu đang gõ mà người chơi nhấn Space → hiện toàn bộ dòng ngay
                StopCoroutine(typingCoroutine);
                dialogueText.text = currentFullLine;
                isTyping = false;
            }
            else
            {
                ShowNextLine();
            }
        }
    }

    private string currentFullLine = "";

    public void ShowDialogue(List<string> lines)
    {
        if (lines == null || lines.Count == 0) return;

        dialogueLines.Clear();
        foreach (var line in lines)
            dialogueLines.Enqueue(line);

        dialogueUI.SetActive(true);
        isDialogueActive = true;
        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (dialogueLines.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentFullLine = dialogueLines.Dequeue();
        typingCoroutine = StartCoroutine(TypeLine(currentFullLine));
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        dialogueUI.SetActive(false);
    }
}
