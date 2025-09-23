using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private int _coinCounter = 0;
    // Start is called before the first frame update
    private void Start()
    {
        // Suscribirse al evento de recogida de monedas
        EventBus<int>.Subscribe(GameEvent.CoinCollected, HandleOnCoinCollected);
        StaticTree.Start();
    }

    /// <summary>
    /// Manejador para el evento de recogida de monedas. Cada vez que una moneda es recogida,
    /// se incrementa el contador de monedas y se verifica si se ha alcanzado el objetivo.
    /// </summary>
    /// <param name="coinValue"></param>
    private void HandleOnCoinCollected(int coinValue)
    {
        _coinCounter += coinValue;
        Debug.Log($"Coins collected: {_coinCounter}");
        if (_coinCounter >= 300)
        {
            Debug.Log("You have collected enough coins!");
            Destroy(gameObject);
        }
    }
}
