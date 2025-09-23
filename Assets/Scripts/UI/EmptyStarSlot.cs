using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EmptyStarSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Feedback (opcional)")]
    [SerializeField] private Image hoverHighlight;

    private Star currentStar;

    public bool IsOccupied => currentStar != null;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        var star = eventData.pointerDrag.GetComponent<Star>();
        if (star == null) return;

        // Si ya tengo una estrella, puedes decidir rechazar o reemplazar.
        if (!IsOccupied || currentStar == star)
        {
            currentStar = star;
            star.SnapTo(this);
        }
        // Si no quieres reemplazar, simplemente no hagas nada cuando IsOccupied == true.
    }

    public void Release()
    {
        currentStar = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverHighlight) hoverHighlight.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverHighlight) hoverHighlight.enabled = false;
    }
}