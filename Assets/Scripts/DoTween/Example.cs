using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Example : MonoBehaviour
{
    public RectTransform panel;
    public RectTransform target;
    public Image image;
    public float position = 500;
    public float duration = 1f;
    public Ease ease = Ease.InExpo;
    public Color color;

    [ContextMenu("MoveRight")]
    public void MoveRight()
    {
        //panel.DOAnchorPosX(position, duration).SetEase(ease);
        panel.DOAnchorPos(target.anchoredPosition, duration).SetEase(ease);
    }
    
    [ContextMenu("Scale")]
    public void Scale()
    {
        panel.DOScale(2f, duration).SetEase(ease).SetLoops(3, LoopType.Yoyo);
    }
    
    [ContextMenu("SetColor")]
    public void SetColor()
    {
        image.DOColor(color, 1f).SetEase(ease);
    }
    
    [ContextMenu("FadeOut")]
    public void FadeOut()
    {
        image.DOFade(0, 1f).SetEase(ease);
    }
    
    [ContextMenu("Sequence")]
    public void Sequence()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(panel.DOAnchorPosX(position, duration).SetEase(ease));
        sequence.Append(panel.DOScale(2f, duration).SetEase(ease).SetLoops(3, LoopType.Yoyo));
        sequence.Join(image.DOColor(color, duration).SetEase(ease));
        sequence.AppendCallback(EndSequence);

        sequence.Play();
    }

    public void EndSequence()
    {
        Debug.Log("Animación terminada!");
    }
}