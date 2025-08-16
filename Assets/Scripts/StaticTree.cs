using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class StaticTree
{
    private static int _coinCounter = 0;
    
    public static void Start()
    {
        EventBus<float>.Subscribe(GameEvent.CoinCollected, HandleOnCoinCollected);
    }
    
    private static void HandleOnCoinCollected(float coinValue)
    {
        _coinCounter += (int)coinValue;
        Debug.Log($"Coins collected STATIC: {_coinCounter}");
        if (_coinCounter >= 300)
        {
            Debug.Log("You have collected enough coins! STATIC");
        }
    }
}
