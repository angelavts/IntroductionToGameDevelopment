using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private int _coinCounter = 0;
    // Start is called before the first frame update
    private void Start()
    {
        SimpleEventBus.Subscribe(GameEvent.CoinCollected, HandleOnCoinCollected);
    }

    private void HandleOnCoinCollected()
    {
        _coinCounter++;
        Debug.Log($"Coins collected: {_coinCounter}");
        if (_coinCounter >= 3)
        {
            Debug.Log("You have collected enough coins!");
            Destroy(gameObject);
        }
    }
}
