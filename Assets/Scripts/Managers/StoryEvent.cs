using System.Collections.Generic;

[System.Serializable]
public class StoryEvent
{
    public string id;
    public string name;
    public string triggerType;
    public string areaId;
    public string objectId;
    public StoryConditions conditions;
    public string dialogueId;
    public string cutsceneId;
    public bool once;
    public List<string> lines;
}

[System.Serializable]
public class StoryConditions
{
    public string hasItem;
    public string questCompleted;
    public int level;
}
