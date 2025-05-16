using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Tham chiếu đến các phần hiển thị")]
    public GameObject chestClosedVisual; // Hình ảnh rương đóng
    public GameObject chestOpenedVisual; // Hình ảnh rương mở

    [Header("Trạng thái")]
    private bool isPlayerInRange = false;
    private bool isOpened = false;

    [Header("Âm thanh")]
    private AudioSource audioSource;

    void Start()
    {
        // Đảm bảo trạng thái ban đầu đúng
        chestClosedVisual.SetActive(true);
        chestOpenedVisual.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isPlayerInRange && !isOpened && Input.GetKeyDown(KeyCode.Space))
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        isOpened = true;
        audioSource.Play();
        // Đổi hiển thị hình ảnh
        chestClosedVisual.SetActive(false);
        chestOpenedVisual.SetActive(true);

        // Nếu có âm thanh, hiệu ứng, item... thêm tại đây
        Debug.Log("Chest opened!");
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Player is near the chest.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Player left the chest area.");
        }
    }
}
