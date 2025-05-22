[System.Serializable]
public class Quest
{
    public string questId;
    public string title;
    public string description;

    public string requiredItem;
    public int requiredAmount;
    public bool isCompleted = false;
}
