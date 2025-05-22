using System.Collections.Generic;

namespace Game.Enums
{
    public enum InteractionType
    {
        Talk,
        Quest = 1,
        Shop,
        Trigger
    }

    [System.Serializable]
    public class NPCData
    {
        public string id;
        public string npcName;
        public List<string> dialogues;
        public InteractionType interactionType;
        public string questId;
    }
}
