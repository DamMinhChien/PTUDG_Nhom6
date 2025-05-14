using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private Dictionary<string, string> mapKeyLookup = new Dictionary<string, string>()
    {
        { "Bến tàu phía Tây", "ben_tau_phia_tay" },
        { "Mỏ quặng tử tinh 1", "mo_quang_tu_tinh_1" },
        { "Mỏ quặng tử tinh 2", "mo_quang_tu_tinh_2" },
        // Thêm các map khác tại đây
    };

    public void EnterMap(string displayName)
    {
        if (mapKeyLookup.ContainsKey(displayName))
        {
            string mapKey = mapKeyLookup[displayName];
            Debug.Log("Đã vào map: " + displayName + " → key: " + mapKey);
            Object.FindFirstObjectByType<StoryManager>()?.ShowStoryForMap(mapKey);
        }
        else
        {
            Debug.LogWarning("Không tìm thấy map key cho map: " + displayName);
        }
    }

}
