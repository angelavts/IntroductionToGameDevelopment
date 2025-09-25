using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryPanelController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Refs")]
    [SerializeField] private RectTransform panel;        // El panel que se mueve
    [SerializeField] private RectTransform tab;          // La pestaña que sobresale
    [SerializeField] private CanvasGroup canvasGroup;    // Para fade opcional

    [Header("Anim")]
    [SerializeField] private float openX = 0f;           // Posición X abierta
    [SerializeField] private float closedX = 420f;       // Posición X cerrada (fuera de pantalla)
    [SerializeField] private float duration = 0.35f;
    [SerializeField] private Ease ease = Ease.OutCubic;
    [SerializeField] private float autoCloseDelay = 0.6f;

    private bool isHovered;
    private bool isOpen;
    private Tween currentTween;
    private float hoverExitTime;

    private void Reset()
    {
        if (!panel) panel = GetComponent<RectTransform>();
        if (!canvasGroup) canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Awake()
    {
        // Inicia cerrado visualmente
        var ap = panel.anchoredPosition;
        panel.anchoredPosition = new Vector2(closedX, ap.y);
        canvasGroup.alpha = 0.7f;
        isOpen = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        Open();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        hoverExitTime = Time.time;
    }

    private void Update()
    {
        // Autocierre si no está en hover y no se está arrastrando un item
        if (!isHovered && isOpen && Time.time - hoverExitTime >= autoCloseDelay && !InventoryItemUI.IsAnyDragging)
        {
            Close();
        }
    }

    public void Open()
    {
        if (isOpen) return;
        Debug.Log("Open panel");
        isOpen = true;
        currentTween?.Kill();
        currentTween = DOTween.Sequence()
            .Join(panel.DOAnchorPosX(openX, duration).SetEase(ease))
            .Join(canvasGroup.DOFade(1f, duration * 0.8f));
    }

    public void Close()
    {
        if (!isOpen) return;
        isOpen = false;
        currentTween?.Kill();
        currentTween = DOTween.Sequence()
            .Join(panel.DOAnchorPosX(closedX, duration).SetEase(ease))
            .Join(canvasGroup.DOFade(0.7f, duration * 0.8f));
    }

    // Para cerrar desde un item cuando regresa
    public void CloseAfter(float delay)
    {
        DOVirtual.DelayedCall(delay, Close);
    }

    // Método útil para la pestaña si la haces Button: asigna OnClick → Toggle
    public void Toggle()
    {
        if (isOpen) Close(); else Open();
    }
}
