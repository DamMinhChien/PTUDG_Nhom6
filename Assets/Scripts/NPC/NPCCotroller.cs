using Game.Interfaces;
using UnityEngine;

public class NPCController : MonoBehaviour, IInteractable
{
    public string npcId; // gán thủ công trong Inspector

    private NPCData npcData;

    private void Start()
    {
        npcData = NPCDataLoader.Instance.GetNPCDataById(npcId);
        if (npcData == null)
            Debug.LogError($"Không tìm thấy dữ liệu cho NPC ID: {npcId}");
    }

    public void Interact()
    {
        if (npcData != null)
            DialogueManager.Instance.ShowDialogue(npcData.dialogues);
    }
}
