using UnityEngine;

public class OnTriggerEnter : MonoBehaviour
{
    public GameObject targetGrid;  // Grid bạn muốn chuyển tới
    public Transform targetPosition; // Vị trí spawn tại map mới

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Tắt tất cả Grid trong MapTho
            foreach (Transform grid in transform.parent)
            {
                grid.gameObject.SetActive(false);
            }

            // Bật Grid mới
            targetGrid.SetActive(true);

            // Di chuyển player
            other.transform.position = targetPosition.position;

            // Reset velocity
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

}
