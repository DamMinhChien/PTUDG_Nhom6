using UnityEngine;
using System.Collections.Generic;

public class NPCDataLoader : MonoBehaviour
{
    public static NPCDataLoader Instance;

    private Dictionary<string, NPCData> npcDataDict;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        LoadNPCData();
    }

    private void LoadNPCData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("npc_data");
        if (jsonFile == null)
        {
            Debug.LogError("Không tìm thấy file npc_data.json trong Resources!");
            return;
        }

        NPCDataList npcList = JsonUtility.FromJson<NPCDataList>(jsonFile.text);

        npcDataDict = new Dictionary<string, NPCData>();
        foreach (var npc in npcList.npcs)
        {
            npcDataDict[npc.id] = npc;
        }
    }

    public NPCData GetNPCDataById(string id)
    {
        if (npcDataDict != null && npcDataDict.ContainsKey(id))
            return npcDataDict[id];
        else
            return null;
    }
}
