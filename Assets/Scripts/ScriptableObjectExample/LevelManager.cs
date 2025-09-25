using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelConfig levelConfig;
    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log($"Nombre del nivel: {levelConfig.LevelName}");
        Debug.Log($"Monedas en este nivel: {levelConfig.MaxCoinAmount}");
        Debug.Log($"Cantidad de vida total: {levelConfig.TotalLife}");
        Debug.Log($"Debes derrotar a {levelConfig.EnemyAmount} enemigos.");
    }
    
}
