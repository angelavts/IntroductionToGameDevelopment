using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayerDropZone : MonoBehaviour, IDropReceiver, IDropHandler
{
    [Header("Snap")]
    [SerializeField] private RectTransform snapTarget; // Dónde “queda” el item dentro del player UI
    [Header("Feedback")]
    [SerializeField] private float pulseScale = 1.05f;
    [SerializeField] private float pulseTime = 0.15f;

    private Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
        img.raycastTarget = true; // necesario para recibir drop
        img.color = new Color(1, 1, 1, 0.1f); // ligera visibilidad
    }

    public void OnDrop(PointerEventData eventData)
    {
        // El flujo principal lo maneja el item: este método puede quedar vacío
        // o puedes duplicar la lógica aquí si prefieres.
    }

    public bool TryEquip(InventoryItemUI item)
    {
        // Aquí validas si el item se puede equipar (tipo, nivel, etc.)
        // Para demo, lo aceptamos siempre:
        return true;
    }

    public Transform GetSnapTransform() => snapTarget != null ? snapTarget : transform;

    public void OnEquipVisual(InventoryItemUI item)
    {
        // Feedback rápido de equipar
        var t = (snapTarget ? snapTarget : (RectTransform)transform);
        t.DOKill();
        t.localScale = Vector3.one;
        t.DOScale(pulseScale, pulseTime).SetLoops(2, LoopType.Yoyo);
        // (Aquí puedes notificar al inventario, actualizar stats, etc.)
    }
}