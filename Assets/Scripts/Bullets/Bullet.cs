using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [Header("Tuning")]
    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifetime = 2.5f;
    [SerializeField] private int damage = 1;
    [Header("Particles")]
    [SerializeField] private ParticleSystem particlePrefab;

    private Rigidbody2D rb;
    private float deathTime;
    private System.Action<Bullet> _onReturnToPool; // callback al pool

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Recomendado para proyectiles
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    public void Fire(Vector2 position, Vector2 direction, float customSpeed, int customDamage, System.Action<Bullet> onReturnToPool)
    {
        transform.position = new Vector3(position.x, position.y, transform.position.z);
        gameObject.SetActive(true);

        _onReturnToPool = onReturnToPool;
        speed = customSpeed > 0 ? customSpeed : speed;
        damage = customDamage > 0 ? customDamage : damage;

        // normaliza y aplica velocidad
        Vector2 dir = direction.sqrMagnitude > 0.0001f ? direction.normalized : Vector2.right;
        rb.velocity = dir * speed;

        deathTime = Time.time + lifetime;
    }

    void Update()
    {
        if (Time.time >= deathTime)
            ReturnToPool();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Aquí se puede aplicar daño a un objetivo
        ParticleSystem particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
        _onReturnToPool?.Invoke(this);
    }
}