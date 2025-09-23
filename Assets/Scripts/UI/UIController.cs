using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button menuButton;
    [SerializeField] private Button infoButton;
    [Header("Main Menu")]
    [SerializeField] private Button closeMenuButton;
    [SerializeField] private Button exitGameButton;
    [Header("Info Menu")]
    [SerializeField] private Button closeInfoButton;
    [Header("Life")]
    [SerializeField] private Image lifeFill;
    
    [Header("Score")][Tooltip("Puntos ganados por el jugador")]
    [SerializeField] private TMP_Text scoreText;

    [Header("References")] 
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject infoMenu;

    private int score;

    private void Awake()
    {
        score = 0;
        menuButton.onClick.AddListener(OpenMainMenu);
        closeMenuButton.onClick.AddListener(CloseMainMenu);
        
        infoButton.onClick.AddListener(OpenInfoButton);
        closeInfoButton.onClick.AddListener(CloseInfoButton);
        
        exitGameButton.onClick.AddListener(ExitGame);
        infoButton.onClick.AddListener(ExitGame);
    }

    private void Start()
    {
        EventBus<int>.Subscribe(GameEvent.PlayerAttacked, HandleOnPlayerAttacked);
        EventBus<int>.Subscribe(GameEvent.PlayerDamaged, HandleOnPlayerDamaged);
    }

    private void HandleOnPlayerDamaged(int remainingLife)
    {
        Debug.Log($"Updating life UI: {remainingLife}");
        Debug.Log($"Updating life UI: {remainingLife / 100f}");
        lifeFill.fillAmount = (float)remainingLife / 100f;
    }

    private void HandleOnPlayerAttacked(int damage)
    {
        score += damage;
        scoreText.text = score.ToString();
    }

    private void OpenMainMenu()
    {
        Time.timeScale = 0f; // Pausa el juego
        mainMenu.SetActive(true);
    }
    
    private void CloseMainMenu()
    {
        Time.timeScale = 1f; // Reanudar el juego
        mainMenu.SetActive(false);
    }
    
    private void OpenInfoButton()
    {
        Time.timeScale = 0f; // Pausa el juego
        infoMenu.SetActive(true);
    }
    
    private void CloseInfoButton()
    {
        Time.timeScale = 1f; // Reanudar el juego
        infoMenu.SetActive(false);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
