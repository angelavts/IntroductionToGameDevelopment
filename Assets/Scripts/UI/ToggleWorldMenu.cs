using UnityEngine;

public class ToggleWorldMenu : MonoBehaviour
{
    public GameObject menuCanvas;   // El menú que se mostrará
    public Transform player;        // El personaje que debe seguir
    public Vector3 offset = new Vector3(0, 0, 0); 

    void Update()
    {
        if (player && menuCanvas.activeSelf)
        {
            // Seguir al jugador con offset
            menuCanvas.transform.position = player.position + offset;
        }
    }
}