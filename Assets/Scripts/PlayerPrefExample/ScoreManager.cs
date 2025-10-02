using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    
    // Keys
    private const string KEY_CURRENT = "GAME_CURRENT_SCORE";
    private const string KEY_HIGH    = "GAME_HIGH_SCORE";
    
    public int CurrentScore { get; private set; }
    public int HighScore    { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        CurrentScore = PlayerPrefs.GetInt(KEY_CURRENT, 0);
        HighScore = PlayerPrefs.GetInt(KEY_HIGH, 0);
    }
    
    public void UpdateScore(int score)
    {
        CurrentScore = score;
        CheckHighScore();
        SaveScore();
    }

    public void AddScore(int score)
    {
        CurrentScore += score;
        CheckHighScore();
        SaveScore();
    }

    private void CheckHighScore()
    {
        if (CurrentScore > HighScore)
        {
            HighScore = CurrentScore;
        }
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt(KEY_CURRENT, CurrentScore);
        PlayerPrefs.SetInt(KEY_HIGH, HighScore);
        PlayerPrefs.Save();
    }

    private void PlayerPrefExample()
    {
        PlayerPrefs.SetInt("SCORE", 0);
        PlayerPrefs.SetInt("COINS", 10);
        PlayerPrefs.Save();
        
        int x = PlayerPrefs.GetInt("COINS");
        Debug.Log($"Score: {CurrentScore}");
    }
}
