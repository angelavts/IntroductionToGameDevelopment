using UnityEngine;

// Permite crear un scriptable object en las carpetas del proyecto
[CreateAssetMenu(fileName = "LevelConfig_", menuName = "Game/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private string levelName;
    [SerializeField] private int maxCoinAmount;
    [SerializeField] private int totalLife;
    [SerializeField] private int enemyAmount;
    
    public string LevelName { get => levelName; set => levelName = value; }
    public int MaxCoinAmount { get => maxCoinAmount; set => maxCoinAmount = value; }
    public int TotalLife { get => totalLife; set => totalLife = value; }
    public int EnemyAmount { get => enemyAmount; set => enemyAmount = value; }
}
