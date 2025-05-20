/*using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC/NPC Data")]
public class NPCData : ScriptableObject
{
    public string npcId;
    public string displayName;
    [TextArea(2, 5)]
    public List<string> dialogues;
    public Sprite portrait;
}
*/
using System.Collections.Generic;

[System.Serializable]
public class NPCData
{
    public string id;
    public string npcName;
    public List<string> dialogues;
    public string interactionType; // Ví dụ: "Talk", "Quest"
}
public class NPCDataList
{
    public List<NPCData> npcs;
}
