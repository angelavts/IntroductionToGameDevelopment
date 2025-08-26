using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    // Animator params (optimizados con hash)
    private static readonly int IsJumpingId = Animator.StringToHash("IsJumping");
    private static readonly int SpeedXId    = Animator.StringToHash("SpeedX");
    private static readonly int AttackId    = Animator.StringToHash("Attack");

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private Camera mainCamera;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.12f;

    [Header("Movimiento")]
    [SerializeField] private float maxSpeed = 7f;
    [SerializeField] private float acceleration = 60f;
    [SerializeField] private float deceleration = 70f;
    [Range(0f, 1f)] [SerializeField] private float airControl = 0.6f;

    [Header("Salto")]
    // Opción A (directa): fuerza/velocidad vertical inicial
    [SerializeField] private float jumpVelocity = 10f;

    // Opción B (salto por métricas): si prefieres altura/tiempo, descomenta y usa CalculateFromMetrics()
    //[SerializeField] private float jumpHeight = 3f;
    //[SerializeField] private float timeToApex = 0.45f;

    [SerializeField] private float coyoteTime = 0.1f;       // ms de perdón tras dejar el borde
    [SerializeField] private float jumpBufferTime = 0.12f;  // ms de buffer si presionas salto un poco antes

    [Header("Gravedad dinámica (feel)")]
    [SerializeField] private float lowJumpGravityMult = 2.0f; // al soltar el botón subiendo
    [SerializeField] private float fallGravityMult = 2.3f;    // al caer
    [SerializeField] private float maxFallSpeed = -20f;

    [Header("Varios")]
    [SerializeField] private bool facingRight = true; // estado visual
    [SerializeField] private bool freezeZRotation = true;
    [SerializeField] private Weapon2D weapon;
    
    
    public bool FacingRight => facingRight;

    // Estados
    private bool isGrounded;
    private bool wasGrounded;
    private bool jumpHeld;
    private bool jumpQueued;
    private bool isAttacking;
    private float lastGroundedTime;
    private float lastJumpPressedTime;
    private float baseGravityScale;

    // Input cache
    private float inputX;

    private void Reset()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        if (freezeZRotation) rb.freezeRotation = true;

        // Gravedad base actual (desde Project Settings)
        baseGravityScale = rb.gravityScale;

        // Recomendado para personajes rápidos
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void Start()
    {
        mainCamera ??= Camera.main;
        
    }

    // Lee inputs SOLO aquí
    private void Update()
    {
        mainCamera.transform.position = new Vector3(transform.position.x, 
            transform.position.y, mainCamera.transform.position.z);
        
        inputX = isAttacking ? 0 : Input.GetAxisRaw("Horizontal"); // -1..1

        if (!isAttacking && Input.GetButtonDown("Jump"))
        {
            jumpQueued = true;
            lastJumpPressedTime = Time.time;
        }
        if (Time.time - lastJumpPressedTime > jumpBufferTime)
            jumpQueued = false;

        jumpHeld = Input.GetButton("Jump");
        
        // --- Ataque ---
        if (!isAttacking && Input.GetButtonDown("Fire1")) // Fire1 es el botón izquierdo del mouse / Ctrl izquierdo
        {
            DoAttack();
        }

        // Flip visual (en Update para evitar jitter)
        HandleFlip(inputX);
    }
    
    private void DoAttack()
    {
        if (!animator) return;
        isAttacking = true;
        animator.SetTrigger(AttackId); // activa animación Attack
        
    }
    
    public void EndAttack()
    {
        isAttacking = false;
        weapon.TryFire();
    }

    // Aplica físicas SOLO aquí
    private void FixedUpdate()
    {
        GroundCheck();

        // Movimiento horizontal con aceleración/desaceleración
        float targetSpeed = inputX * maxSpeed;
        float accel = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : deceleration;
        if (!isGrounded) accel *= airControl;

        float newVelX = Mathf.MoveTowards(rb.velocity.x, targetSpeed, accel * Time.fixedDeltaTime);

        // Saltar (coyote + buffer)
        bool canCoyote = (Time.time - lastGroundedTime) <= coyoteTime;
        if ((isGrounded || canCoyote) && jumpQueued)
        {
            jumpQueued = false; // consumir buffer
            rb.velocity = new Vector2(newVelX, jumpVelocity);
            isGrounded = false; // entramos en aire
        }
        else
        {
            // Gravedad dinámica (salto variable y caída pesada)
            float gScale = baseGravityScale;
            if (rb.velocity.y > 0f && !jumpHeld) gScale *= lowJumpGravityMult;      // cortar salto
            else if (rb.velocity.y < 0f)        gScale *= fallGravityMult;          // caída pesada
            rb.gravityScale = gScale;

            // clamp velocidad de caída
            float vy = Mathf.Max(rb.velocity.y, maxFallSpeed);
            rb.velocity = new Vector2(newVelX, vy);
        }

        // Animator
        UpdateAnimator();
    }

    private void GroundCheck()
    {
        wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            lastGroundedTime = Time.time;
            // restaurar gravedad base al aterrizar
            rb.gravityScale = baseGravityScale;
        }
    }

    private void UpdateAnimator()
    {
        if (!animator) return;

        // SpeedX: usa magnitud horizontal suave (para blendtrees)
        animator.SetFloat(SpeedXId, Mathf.Abs(rb.velocity.x));

        // IsJumping: true si no está en suelo o si va hacia arriba
        bool isJumpingAnim = !isGrounded && rb.velocity.y > 0.01f;
        animator.SetBool(IsJumpingId, isJumpingAnim);
    }

    private void HandleFlip(float x)
    {
        if (x > 0.01f && !facingRight) SetFacing(true);
        else if (x < -0.01f && facingRight) SetFacing(false);
    }

    private void SetFacing(bool right)
    {
        facingRight = right;
        // Fallback: invertir escala X
        Vector3 sc = transform.localScale;
        sc.x = Mathf.Abs(sc.x) * (right ? 1f : -1f);
        transform.localScale = sc;
    }
}
