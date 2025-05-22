using UnityEngine;
using UnityEngine.Tilemaps;

public class MapPortal : MonoBehaviour
{
    public GameObject targetMap;
    public Transform targetPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Tắt tất cả các game obj trong Map trong scene
            GameObject map = GameObject.FindGameObjectWithTag("Map");
            if (map != null)
            {
                foreach (Transform child in map.transform)
                {
                    foreach (Transform grandChild in child)
                    {
                        grandChild.gameObject.SetActive(false);
                    }
                }

                // Gỡ tag active_zone khỏi map cũ (nếu có)
                GameObject oldActive = GameObject.FindGameObjectWithTag("active_zone");
                if (oldActive != null)
                {
                    oldActive.tag = "Untagged";
                }
            }
            else
            {
                Debug.LogWarning("Không tìm thấy Map trong scene!");
            }

            // Bật map mới
            targetMap.SetActive(true);
            GameManager.Instance.SetCurrentMap(targetMap);

            // Tìm phần tử có tag "not_active" bên trong targetMap và đổi tag thành "active_zone"
            Transform[] children = targetMap.GetComponentsInChildren<Transform>(true);
            bool found = false;
            foreach (Transform child in children)
            {
                if (child.CompareTag("not_active"))
                {
                    child.tag = "active_zone";
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Debug.LogWarning("Không tìm thấy phần tử với tag 'not_active' trong targetMap.");
            }

            // Cập nhật collider mới cho camera
            FindFirstObjectByType<CameraBoundsManager>()?.UpdateCameraBounds();
            //var something = FindFirstObjectByType<SomeType>();

            // Di chuyển player
            other.transform.position = targetPosition.position;

            // Reset velocity
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }

            Debug.Log("Teleported to: " + targetMap.name);
        }
    }
}
