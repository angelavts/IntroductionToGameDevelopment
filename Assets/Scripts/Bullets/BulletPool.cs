using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private int prewarmCount = 20;
    [SerializeField] private int maxPoolSize = 100; // 0 = sin límite

    private readonly Queue<Bullet> _pool = new Queue<Bullet>();

    void Awake()
    {
        Prewarm();
    }

    private void Prewarm()
    {
        for (int i = 0; i < prewarmCount; i++)
        {
            Bullet b = CreateNew();
            b.gameObject.SetActive(false);
            _pool.Enqueue(b);
        }
    }

    private Bullet CreateNew()
    {
        var b = Instantiate(bulletPrefab, transform);
        b.gameObject.SetActive(false);
        return b;
    }

    public Bullet Get()
    {
        if (_pool.Count > 0) return _pool.Dequeue();

        if (maxPoolSize == 0 || transform.childCount < maxPoolSize)
            return CreateNew();

        // Pool lleno: opcionalmente, reutiliza el más antiguo o retorna null
        // Aquí reusamos creando de todos modos para no perder disparo:
        return CreateNew();
    }

    public void Return(Bullet b)
    {
        if (!b) return;
        b.transform.SetParent(transform);
        b.gameObject.SetActive(false);
        _pool.Enqueue(b);
    }
}