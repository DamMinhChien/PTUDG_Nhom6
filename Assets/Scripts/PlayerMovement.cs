using UnityEngine;
using System;
public class PlayerMovement : MonoBehaviour
{
    public event Action OnEncountered;
    public bool isEnableMove { get; set; }
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDir = Vector2.down; // Mặc định hướng nhìn xuống

    private Animator animator;
    private Collider2D npcCollider;

    void Start()
    {
        isEnableMove = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void HandleUpdate()
    {
        // Nhập phím
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        // Nếu đang di chuyển → cập nhật hướng cuối
        if (moveInput != Vector2.zero)
        {
            lastMoveDir = moveInput;
        }

        // Luôn dùng hướng cuối để điều khiển Animator
        animator.SetFloat("MoveX", lastMoveDir.x);
        animator.SetFloat("MoveY", lastMoveDir.y);
        animator.SetBool("IsMoving", moveInput != Vector2.zero);
    }

    void FixedUpdate()
    {
        if (isEnableMove)
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            npcCollider = other.collider;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("NPC") && npcCollider == other.collider)
        {
            npcCollider = null;
        }
    }

    void Update()
    {
        if (npcCollider != null && Input.GetKeyDown(KeyCode.E))
        {
            OnEncountered?.Invoke();
        }
    }
}
