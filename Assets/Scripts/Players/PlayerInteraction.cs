using Game.Interfaces;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRadius = 2f;
    public LayerMask interactableLayer;

    public GameObject interactHintUI; // Giao diện hướng dẫn

    private IInteractable nearbyInteractable;

    void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRadius, interactableLayer);

        nearbyInteractable = null;
        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                var interactable = hit.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    nearbyInteractable = interactable;
                    break;
                }
            }
        }

        // Hiển thị UI nếu có NPC gần
        if (interactHintUI != null)
        {
            bool shouldShowHint = nearbyInteractable != null && !DialogueManager.Instance.IsDialogueActive;
            interactHintUI.SetActive(shouldShowHint);
        }


        if (nearbyInteractable != null && Input.GetKeyDown(KeyCode.E))
        {

            if (interactHintUI != null)
                interactHintUI.SetActive(false);
            
            nearbyInteractable.Interact();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}

