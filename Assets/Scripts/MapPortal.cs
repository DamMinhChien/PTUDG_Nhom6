using UnityEngine;

public class MapPortal : MonoBehaviour
{
    public GameObject targetGrid;
    public Transform targetPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something entered trigger: " + other.name);
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
            Debug.Log("Teleported to: " + targetGrid.name);
        }
    }

}
