using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trigger with Player detected!");
            // Publicar en el bus de eventos el evento que indica que se ha recogido una moneda
            EventBus<int>.Publish(GameEvent.CoinCollected, coinValue);
            Destroy(gameObject); 
        }
    }
}
