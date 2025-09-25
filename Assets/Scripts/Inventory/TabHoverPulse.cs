using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class TabHoverPulse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float s = 1.08f;
    [SerializeField] private float t = 0.2f;
    private Tween tw;

    public void OnPointerEnter(PointerEventData e)
    {
        tw?.Kill();
        tw = transform.DOScale(s, t).SetLoops(-1, LoopType.Yoyo);
    }

    public void OnPointerExit(PointerEventData e)
    {
        tw?.Kill();
        transform.localScale = Vector3.one;
    }
}