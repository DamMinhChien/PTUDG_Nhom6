using UnityEngine;

public class OnTriggerEnter : MonoBehaviour
{
    public string mapDisplayName; // VD: "Bến tàu phía Tây"

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player vào map: " + mapDisplayName);
            MapManager mm = Object.FindFirstObjectByType<MapManager>();
            if (mm != null)
                mm.EnterMap(mapDisplayName);
            else
                Debug.LogError("Không tìm thấy MapManager!");
        }
    }
}
