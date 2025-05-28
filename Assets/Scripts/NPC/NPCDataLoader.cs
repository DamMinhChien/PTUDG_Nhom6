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

    /*private void LoadNPCData()
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
    }*/

    private void LoadNPCData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("npc_data");
        if (jsonFile == null)
        {
            Debug.LogError("❌ Không tìm thấy file npc_data.json trong Resources!");
            return;
        }

        Debug.Log("✅ Tìm thấy file npc_data.json, nội dung:\n" + jsonFile.text);

        NPCDataList npcList = JsonUtility.FromJson<NPCDataList>(jsonFile.text);
        if (npcList == null || npcList.npcs == null)
        {
            Debug.LogError("❌ Không thể parse JSON hoặc danh sách NPC trống.");
            return;
        }

        npcDataDict = new Dictionary<string, NPCData>();
        foreach (var npc in npcList.npcs)
        {
            Debug.Log($"✅ Đã load NPC: {npc.id} - {npc.npcName}");
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
