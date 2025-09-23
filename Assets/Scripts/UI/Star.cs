using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Star : MonoBehaviour, 
    IPointerEnterHandler, IPointerExitHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    [SerializeField] private Image highlight;

    [Header("Drag")]
    [SerializeField] private Canvas canvas;  // Asigna tu Canvas principal (o se busca en Awake)
    [SerializeField] private float returnSpeed = 15f;

    private RectTransform rect;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    private Vector3 originalPosition;
    private bool snapped;
    private bool isDragging;  
    private EmptyStarSlot currentSlot;

    void Awake()
    {
        if (!rect) rect = GetComponent<RectTransform>();
        if (!canvasGroup) canvasGroup = GetComponent<CanvasGroup>();
        if (!canvas) canvas = GetComponentInParent<Canvas>();

        if (highlight) highlight.enabled = false;

        // Guarda parent/posición original para “volver”
        originalParent = rect.parent;
        originalPosition = rect.anchoredPosition;
    }

    #region Hover Highlight
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (highlight) highlight.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isDragging && highlight) highlight.enabled = false; // no apagar si estamos arrastrando
    }
    #endregion

    #region Drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        snapped = false;
        // Permite que los slots reciban el raycast durante el drag
        if (canvasGroup) canvasGroup.blocksRaycasts = false;

        // (Opcional) Sube en la jerarquía temporalmente para dibujar arriba
        rect.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas == null) return;

        // Mover en espacio de UI respetando la escala del Canvas
        // Mover el RectTransform en coordenadas locales de UI.
        // eventData.delta = desplazamiento del puntero en píxeles desde el último frame.
        // Como el Canvas puede estar escalado (CanvasScaler), dividimos entre canvas.scaleFactor
        // para que el movimiento se mantenga consistente y no dependa de la resolución o el zoom del Canvas.
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        // Se decidirá en el Slot (OnDrop) si hace snap. 
        // Si aquí no quedó “snapped”, vuelve a origen.
        if (!snapped)
        {
            ReturnToOrigin();
        }

        if (canvasGroup) canvasGroup.blocksRaycasts = true;
        bool pointerOverSelf = RectTransformUtility.RectangleContainsScreenPoint(
            rect, Input.mousePosition, canvas ? canvas.worldCamera : null
        );
        if (highlight) highlight.enabled = pointerOverSelf;
    }
    #endregion

    public void SnapTo(EmptyStarSlot slot)
    {
        // Liberar slot previo si existía
        if (currentSlot && currentSlot != slot)
            currentSlot.Release();

        currentSlot = slot;
        snapped = true;

        rect.SetParent(slot.transform, worldPositionStays: false);
        rect.anchoredPosition = Vector2.zero;
        rect.localRotation = Quaternion.identity;
        rect.localScale = Vector3.one;
    }

    public void ReturnToOrigin()
    {
        // Si estaba en un slot, liberarlo
        if (currentSlot)
        {
            currentSlot.Release();
            currentSlot = null;
        }

        rect.SetParent(originalParent, worldPositionStays: false);
        StopAllCoroutines();
        StartCoroutine(SmoothReturn(originalPosition));
    }

    private System.Collections.IEnumerator SmoothReturn(Vector3 target)
    {
        // Lerp suave de vuelta
        while ((rect.anchoredPosition - (Vector2)target).sqrMagnitude > 0.1f)
        {
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, target, Time.unscaledDeltaTime * returnSpeed);
            yield return null;
        }
        rect.anchoredPosition = target;
    }
}
