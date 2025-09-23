using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterMenuTrigger : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject menuCanvas; // el Canvas en World Space
    [SerializeField] private Button menuButton; 
    
    private void Awake()
    {
        if (menuButton != null)
        {
            menuButton.onClick.AddListener(HandleOnButtonClicked);
        }
    }
    
    public void HandleOnButtonClicked()
    {
        Debug.Log("Green button clicked");
        EventBus<int>.Publish(GameEvent.PlayerMenuButtonCliked, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (menuCanvas != null)
        {
            // Alternar visibilidad
            menuCanvas.SetActive(!menuCanvas.activeSelf);
        }
    }
}

