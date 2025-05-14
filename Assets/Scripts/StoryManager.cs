using System.Collections;
using UnityEngine;
using TMPro;

public class StoryManager : MonoBehaviour
{
    public GameObject storyPanel;
    public TextMeshProUGUI dialogueText;

    public float typingSpeed = 0.05f;

    private string[] storyLines;
    private int currentLineIndex = 0;
    private bool isTyping = false;

    void Awake()
    {
        storyPanel.SetActive(false); // Ẩn hộp thoại lúc đầu
    }

    // Phương thức ShowStoryForMap để hiển thị cốt truyện cho mỗi map
    public void ShowStoryForMap(string mapKey)
    {
        LoadStoryFromJSON(mapKey.ToLower()); // Ví dụ: "map1"
        if (storyLines.Length > 0)
        {
            currentLineIndex = 0;
            storyPanel.SetActive(true);
            StartCoroutine(TypeLine());
        }
    }

    public void ShowStoryForMap1(string mapKey)
    {
        Debug.Log("Đang gọi ShowStory cho map: " + mapKey);
        Debug.Log("===> ShowStoryForMap đang chạy với mapKey = " + mapKey);
        LoadStoryFromJSON(mapKey.ToLower());

        if (storyLines != null && storyLines.Length > 0)
        {
            Debug.Log("Số dòng cốt truyện: " + storyLines.Length);
            currentLineIndex = 0;
            storyPanel.SetActive(true);
            StartCoroutine(TypeLine());
        }
        else
        {
            Debug.LogWarning("Cốt truyện không có nội dung.");
        }
    }

    void LoadStoryFromJSON(string mapKey)
    {
        string path = "Stories/" + mapKey;
        TextAsset jsonText = Resources.Load<TextAsset>(path);

        if (jsonText != null)
        {
            StoryData data = JsonUtility.FromJson<StoryData>(jsonText.text);
            Debug.Log("Đã load JSON cho " + mapKey);
            storyLines = data.lines;
        }
        else
        {
            Debug.LogWarning("Không tìm thấy cốt truyện cho: " + mapKey);
            storyLines = new string[] { };           
        }
    }
/*    void Start()
    {
        ShowStoryForMap("ben_tau_phia_tay");
    }*/

    void Update()
    {
        Debug.Log("StoryManager đang chạy Update...");
        if (storyPanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = storyLines[currentLineIndex];
                isTyping = false;
            }
            else
            {
                currentLineIndex++;
                if (currentLineIndex < storyLines.Length)
                {
                    StartCoroutine(TypeLine());
                }
                else
                {
                    dialogueText.text = "";
                    storyPanel.SetActive(false);
                }
            }
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in storyLines[currentLineIndex])
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
}
