using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float playerDamage;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;

    public InventoryManagement InventoryManager;
    public HealthManager HealthManager;
    
    private Rigidbody2D rb;
    private Animator animator;
    private Transform tf;
    private Vector2 moveInput;
    
    private float size;
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
                
        size = Mathf.Pow(InventoryManager.GetInventorySize(), 1.0f / 3.0f );
        tf.localScale = new Vector3(Mathf.Sign(tf.localScale.x) * size, size, 1);
    }

    private void Update() {
        playerDamage = HealthManager.playerAttack;

        UpdateAnimator();
        FlipSprite();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void FlipSprite() {
        float horizontalInput = moveInput.x;
        if (Mathf.Abs(horizontalInput) > Mathf.Epsilon) {
            tf.localScale = new Vector3(Mathf.Sign(horizontalInput) * size, size, 1);
        }
    }

    public void Move() {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
    }

    public void UpdateAnimator() {
        isRunning = (Mathf.Abs(rb.velocity.x) > 0.05f || Mathf.Abs(rb.velocity.y) > 0.05f);
        isIdling = (Mathf.Abs(rb.velocity.x) <= 0.05f && Mathf.Abs(rb.velocity.y) <= 0.05f);

        animator.SetBool("isWalking", isRunning);
        animator.SetBool("isIdle", isIdling);
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] attacked = Physics2D.OverlapCircleAll(origin, transform.localScale.x, enemyLayer);

        foreach (Collider2D c in attacked)
        {
            Debug.Log("Attack hit!");
            HealthManager.DealDamage(c.gameObject, playerDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x);
    }
}