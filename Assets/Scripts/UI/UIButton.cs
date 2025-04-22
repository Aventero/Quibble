using UnityEngine;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour
{
    private Tween tween;
    private TweenDifficulty tweenDifficulty;

    private bool useTween;

    private void Awake()
    {
        tween = GetComponent<Tween>();
        tweenDifficulty = GetComponent<TweenDifficulty>();

        if (tween != null)
            useTween = true;
    }

    public void OnPointerEnter(BaseEventData eventData)
    {
        UIManager.Instance.OnPointerEnter(gameObject);
        
        if (useTween)
            tween.OnMouseEnter();
        else
            tweenDifficulty.OnMouseEnter();
    }

    public void OnPointerExit(BaseEventData eventData)
    {
        UIManager.Instance.OnPointerExit(gameObject);
        
        if (useTween)
            tween.OnMouseExit();
        else
            tweenDifficulty.OnMouseExit();
    }

    public void OnPointerClick(BaseEventData eventData)
    {
        if (useTween)
            tween.OnMouseClick();
        else
            tweenDifficulty.OnMouseClick();
    }

    public void OnPointerDown(BaseEventData eventData)
    {
        if (useTween)
            tween.OnMouseDown();
        else
            tweenDifficulty.OnMouseDown();
    }

    public void OnPointerUp(BaseEventData eventData)
    {
        if (useTween)
            tween.OnMouseUp();
        else
            tweenDifficulty.OnMouseUp();
    }

}
