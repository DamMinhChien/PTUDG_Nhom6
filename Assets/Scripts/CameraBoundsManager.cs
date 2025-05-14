using Unity.Cinemachine;
using UnityEngine;

public class CameraBoundsManager : MonoBehaviour
{
    private CinemachineConfiner2D confiner2D;

    void Awake()
    {
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }

    void Start()
    {
        UpdateCameraBounds(); // gọi lúc đầu
    }

    // Sau khi bật bản đồ mới nhớ gọi
    //FindObjectOfType<CameraBoundsManager>()?.UpdateCameraBounds();

    public void UpdateCameraBounds()
    {
        Collider2D activeZoneCollider = GameObject.FindGameObjectWithTag("active_zone")?.GetComponent<Collider2D>();
        if (activeZoneCollider != null)
        {
            confiner2D.BoundingShape2D = activeZoneCollider;
            confiner2D.InvalidateBoundingShapeCache(); // RẤT QUAN TRỌNG: cập nhật lại collider
            //confiner2D.InvalidateCache(); lỗi thời
        }
        else
        {
            Debug.LogWarning("Không tìm thấy Collider2D với tag 'active_zone'");
        }
    }
}
