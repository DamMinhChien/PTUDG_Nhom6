using Game.Enums;
using System.Collections.Generic;

[System.Serializable]
public class NPCData
{
    public string id;
    public string npcName;
    public string questId;
    public List<string> dialogues;
    public InteractionType interactionType; // Ví dụ: "Talk", "Quest"
}
[System.Serializable]
public class NPCDataList
{
    public List<NPCData> npcs;
}
