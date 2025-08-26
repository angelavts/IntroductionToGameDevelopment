using UnityEngine;

public class Weapon2D : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private BulletPool pool;
    [SerializeField] private Transform muzzle; // punto de salida de la bala (hijo del Player)
    [SerializeField] private Player player;    // para saber facingRight (o pásalo como bool)

    [Header("Tuning")]
    [SerializeField] private float bulletSpeed = 14f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float fireCooldown = 0.2f;

    private float nextFireTime;

    public void TryFire()
    {
        if (Time.time < nextFireTime) return;

        var bullet = pool.Get();
        if (bullet == null) return;

        // dirección según tu facingRight; o calcula con mouse/aim si quieres
        Vector2 dir = player != null && player.FacingRight ? Vector2.right : Vector2.left;

        bullet.Fire(
            position: muzzle.position,
            direction: dir,
            customSpeed: bulletSpeed,
            customDamage: bulletDamage,
            onReturnToPool: pool.Return
        );

        nextFireTime = Time.time + fireCooldown;
    }
}