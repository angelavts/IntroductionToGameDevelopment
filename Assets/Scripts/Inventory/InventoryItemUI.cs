using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Drag")]
    [SerializeField] private Canvas rootCanvas;     // Canvas principal (para calcular delta)
    [SerializeField] private RectTransform dragLayer; // Capa superior para arrastrar
    [SerializeField] private float returnDuration = 0.25f;
    [SerializeField] private Ease returnEase = Ease.OutQuad;

    private RectTransform rect;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    private Vector2 originalAnchoredPos;
    private Tween moveTween;

    public static bool IsAnyDragging { get; private set; }

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (!canvasGroup) canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        IsAnyDragging = true;
        originalParent = transform.parent;
        originalAnchoredPos = rect.anchoredPosition;
        canvasGroup.blocksRaycasts = false; // Permite que los dropzones reciban el evento
        moveTween?.Kill();

        // Re-parent al dragLayer para que quede por encima
        transform.SetParent(dragLayer, worldPositionStays: true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Mover en espacio de UI correctamente
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            dragLayer as RectTransform, eventData.position, rootCanvas.worldCamera, out var localPoint);
        rect.localPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        IsAnyDragging = false;
        canvasGroup.blocksRaycasts = true;

        // ¿Se soltó sobre un dropzone válido?
        var receiver = GetDropReceiverUnderPointer(eventData);
        if (receiver != null && receiver.TryEquip(this))
        {
            // Éxito: puedes destruir el ítem, cambiar icono, etc.
            // Ejemplo: Snap visual al dropzone y opcionalmente cerrar panel
            SnapTo(receiver.GetSnapTransform());
            receiver.OnEquipVisual(this);
        }
        else
        {
            // Falló → regresar animado a su lugar y re-parent
            ReturnToOriginAndClosePanel();
        }
    }

    private IDropReceiver GetDropReceiverUnderPointer(PointerEventData eventData)
    {
        // Busca un componente IDropReceiver en el objeto bajo el puntero
        if (eventData.pointerEnter == null) return null;
        return eventData.pointerEnter.GetComponentInParent<IDropReceiver>();
    }

    private void ReturnToOriginAndClosePanel()
    {
        // Volver a su grid original
        transform.SetParent(originalParent, worldPositionStays: true);
        moveTween?.Kill();
        moveTween = rect.DOAnchorPos(originalAnchoredPos, returnDuration).SetEase(returnEase)
            .OnComplete(() =>
            {
                // Cerrar panel luego de regresar
                var panel = GetComponentInParent<InventoryPanelController>();
                if (panel != null)
                    panel.CloseAfter(0.05f);
            });
    }

    private void SnapTo(Transform target)
    {
        // Mover hacia un snap target (ej. slot del player)
        transform.SetParent(target, worldPositionStays: false);
        rect.anchoredPosition = Vector2.zero;
        rect.localScale = Vector3.one;
    }
}
