using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public InventoryManagement InventoryManager;
    
    private int size;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform tf;
    private Vector2 moveInput;
    private bool isGrounded;
    private bool isRunning;
    private bool isIdling;
    private bool isFrozen;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        tf = GetComponent<Transform>();
    }

    private void FixedUpdate() {
        Move();
    }

    private void Update() {
        UpdateAnimator();
        FlipSprite();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void FlipSprite() {
        float horizontalInput = moveInput.x;
        if (Mathf.Abs(horizontalInput) > Mathf.Epsilon) {
            tf.localScale = new Vector3(Mathf.Sign(horizontalInput) * Mathf.Abs(tf.localScale.x), 1, 1);
        }
    }

    public void Move() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    public void UpdateAnimator() {
        isRunning = (Mathf.Abs(rb.velocity.x) > 0.2f) && isGrounded;
        isIdling = (Mathf.Abs(rb.velocity.x) <= 0.2f) && isGrounded;
        isFrozen = !isGrounded;
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isIdling", isIdling);
        animator.SetBool("isFrozen", isFrozen);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}