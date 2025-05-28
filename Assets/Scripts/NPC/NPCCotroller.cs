using Game.Enums;
using Game.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, IInteractable
{
    public string npcId;
    private NPCData npcData;
    private Quest pendingQuest;

    private void Start()
    {
        npcData = NPCDataLoader.Instance.GetNPCDataById(npcId);
        if (npcData == null)
        {
            Debug.LogError($"Không tìm thấy dữ liệu cho NPC ID: {npcId}");
            return;
        }
        /*else
        {
            Debug.Log($"✅ Đã load NPC: {npcData.npcName}, Quest: {npcData.questId}");   
        }*/
    }

    public void Interact()
    {
        Debug.Log($"👉 Đã gọi Interact() của NPC: {npcId}");
        if (npcData == null) return;

        switch (npcData.interactionType)
        {
            case InteractionType.Talk:
                DialogueManager.Instance.ShowDialogue(npcData.dialogues);
                break;

            case InteractionType.Shop:
                Debug.Log("Mở giao diện Shop ở đây...");
                // ShopManager.Instance.OpenShop(npcId); // nếu có
                break;


            case InteractionType.Trigger:
                Debug.Log("Kích hoạt sự kiện đặc biệt...");
                // EventManager.Trigger("Event_01");
                break;


            case InteractionType.Quest:
                Quest quest = QuestLoader.Instance.GetQuestById(npcData.questId);

                Debug.Log($"⚠️ Đang xử lý quest tương tác với NPC {npcData.npcName} - Quest: {quest?.questId}");

                if (quest != null && !QuestManager.Instance.HasQuest(quest.questId))
                {
                    pendingQuest = quest;
                    // 👉 CHỈ GỌI MỘT LẦN
                    DialogueManager.Instance.ShowDialogue(npcData.dialogues, OnDialogueFinished);
                }
                else if (quest != null && quest.isCompleted)
                {
                    DialogueManager.Instance.ShowDialogue(new List<string>
                    {
                        "Bạn đã hoàn thành nhiệm vụ này!"
                    });
                }
                else
                {
                    DialogueManager.Instance.ShowDialogue(new List<string>
                    {
                        "Bạn đang thực hiện nhiệm vụ này!"
                    });
                }
                break;


        }
    }
    private IEnumerator ShowQuestOffer(Quest quest)
    {
        // Chờ 1 frame để đảm bảo UI được reset
        yield return new WaitForEndOfFrame();
        Debug.Log("Hiển thị dòng nhiệm vụ");

        DialogueManager.Instance.ShowDialogue(new List<string>
        {
            $"[NHIỆM VỤ] {quest.title}",
            quest.description,
            "Bạn có muốn nhận nhiệm vụ này không? (Nhấn Y để nhận)"
        });

        StartCoroutine(WaitForAcceptKey(quest));
    }

    private IEnumerator WaitForAcceptKey(Quest quest)
    {
        Debug.Log("Đang chờ người chơi nhấn Y...");

        while (!Input.GetKeyDown(KeyCode.Y))
        {
            yield return null;
        }

        Debug.Log("Người chơi đã nhấn Y!");

        QuestManager.Instance.AddQuest(quest);
        DialogueManager.Instance.ShowDialogue(new List<string>
        {
            "Bạn đã nhận nhiệm vụ!"
        });
    }

    private void OnDialogueFinished()
    {
        Debug.Log($"➡️ OnDialogueFinished được gọi - pendingQuest: {(pendingQuest != null ? pendingQuest.questId : "NULL")}");
        Debug.Log("Callback thoại kết thúc - chuẩn bị hiện nhiệm vụ");
        StartCoroutine(ShowQuestOfferDelayed(pendingQuest));
    }

    private IEnumerator ShowQuestOfferDelayed(Quest quest)
    {
        Debug.Log("Coroutine ShowQuestOfferDelayed bắt đầu");
        // Đợi 1 frame để đảm bảo UI được đóng hoàn toàn
        yield return null;

        DialogueManager.Instance.ShowDialogue(new List<string>
        {
            $"[NHIỆM VỤ] {quest.title}",
            quest.description,
            "Bạn có muốn nhận nhiệm vụ này không? (Nhấn Y để nhận)"
        });

        Debug.Log("📝 Đã hiển thị nội dung nhiệm vụ");

        StartCoroutine(WaitForAcceptKey(quest));
    }

}

