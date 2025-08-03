using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleRigidbody2DMovement : MonoBehaviour
{
    [Header("Rigidbody2D Component")]
    [SerializeField] private Rigidbody2D rb;
    
    [Header("Parameters")]
    [SerializeField] private bool facingRight = false;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private float moveSpeed = 5f;
    
    [Header("References")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    private bool isGrounded;
    private int moveX;
    
    void Start()
    {
        if(rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component not found on this GameObject.");
            }
        }
    }

    void Update()
    {
        // Verifica si está tocando el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Saltar si está en el suelo y se presiona espacio
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        
        // Detectar flip visual
        
        if (moveX > 0 && !facingRight)
            Flip();
        else if (moveX < 0 && facingRight)
            Flip();
    }

    void FixedUpdate()
    {
        moveX = (int)Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
    }
    
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;  // Invierte la escala en X
        transform.localScale = scale;
    }
}