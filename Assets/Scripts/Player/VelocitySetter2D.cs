using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class VelocitySetter2D : MonoBehaviour
{
    [Header("Velocidad deseada")]
    public float velocityX = 0f;
    public float velocityY = 0f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(velocityX, velocityY);
    }
}