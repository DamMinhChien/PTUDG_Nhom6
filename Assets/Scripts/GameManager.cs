using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject currentMap;

    private void Awake()
    {
        // Đảm bảo chỉ có một GameManager tồn tại
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ lại qua các scene nếu cần
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Hàm để thay đổi map hiện tại
    public void SetCurrentMap(GameObject newMap)
    {
        currentMap = newMap;
    }

    // Hàm để lấy map hiện tại
    public GameObject GetCurrentMap()
    {
        return currentMap;
    }
}
