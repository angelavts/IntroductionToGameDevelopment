using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trigger with Player detected!");
            SimpleEventBus.Publish(GameEvent.CoinCollected);
            Destroy(gameObject); 
        }
    }
}
