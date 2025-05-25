using System.Collections.Generic;

[System.Serializable]
public class Quest
{
    public string questId;
    public string title;
    public string description;

    public string requiredItem;
    public int requiredAmount;
    public bool isCompleted = false;

    [System.Serializable]
    private class QuestListWrapper
    {
        public List<Quest> quests;
    }
}

