using UnityEngine;

public class Player : MonoBehaviour
{
    // Se usa el string to hash para optimizar el acceso a los parámetros del Animator
    private static readonly int IsJumpingId = Animator.StringToHash("IsJumping");
    private static readonly int SpeedXId = Animator.StringToHash("SpeedX");

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
    [SerializeField] private Animator animator;

    private bool isGrounded;
    private bool isJumping;
    private int moveX;
    
    void Start()
    {
        if (rb == null)
        {
            // Intenta obtener el Rigidbody2D del GameObject
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
            isJumping = true;
            // Actualizar el estado del salto en el Animator
            animator.SetBool(IsJumpingId, isJumping);
        }
        
        if(rb.velocity.y <= 0 && isJumping)
        {
            isJumping = false;
            // Actualizar el estado del salto en el Animator
            animator.SetBool(IsJumpingId, false);
        }
        
        // Detectar flip visual
        if (moveX > 0 && !facingRight)
            Flip();
        else if (moveX < 0 && facingRight)
            Flip();
    }

    void FixedUpdate()
    {
        // Movimiento horizontal del jugador
        moveX = (int)Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        // Actualizar el parámetro de velocidad en el Animator
        // para que se ejecute la animación de caminar
        animator.SetInteger(SpeedXId, moveX);
    }
    
    void Flip()
    {
        // Cambia la dirección del sprite al invertir la escala en X
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;  // Invierte la escala en X
        transform.localScale = scale;
    }
}
