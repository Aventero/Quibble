using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public bool activateSubMenu = false;
    public bool deactivateSubMenu = false;
    [CanBeNull] public ControllerAutoSelect submenuAutoSelect;
    
    private Tween tween;
    private SlotTween slotTween;
    private TweenDifficulty tweenDifficulty;
    private bool active = false;
    
    enum ButtonType
    {
        None,
        Tween,
        SlotTween,
        TweenDifficulty,
    }

    private ButtonType buttonType;

    private void Start()
    {
        tween = GetComponent<Tween>();
        tweenDifficulty = GetComponent<TweenDifficulty>();
        slotTween = GetComponent<SlotTween>();
        
        if(tween != null)
            buttonType = ButtonType.Tween;
        else if (tweenDifficulty != null)
            buttonType = ButtonType.TweenDifficulty;
        else if (slotTween != null)
            buttonType = ButtonType.SlotTween;
        else
            Debug.LogError("UIButton has no tween or tween difficulty or slot Tween");

        if (active) {
            OnPointerEnter(null);
            active = false;
        }
    }

    public void OnPointerEnter(BaseEventData eventData)
    {
        if (buttonType == ButtonType.None) {
            active = true;
            return;
        }
        
        UIManager.Instance.OnPointerEnter(gameObject);

        if (buttonType == ButtonType.Tween)
            tween.OnMouseEnter();
        else if (buttonType == ButtonType.TweenDifficulty)
            tweenDifficulty.OnMouseEnter();
        else
            slotTween.OnEnter();
    }

    public void OnPointerExit(BaseEventData eventData, bool callUIManager = true)
    {
        if(callUIManager)
            UIManager.Instance.OnPointerExit(gameObject);
        
        if (buttonType == ButtonType.Tween)
            tween.OnMouseExit();
        else if(buttonType == ButtonType.TweenDifficulty)
            tweenDifficulty.OnMouseExit();
        else
            slotTween.OnExit();
    }

    // Wrapper method so it can be called from a button event
    public void OnPointerExit(BaseEventData eventData)
    {
        OnPointerExit(eventData, true);
    }

    public void OnPointerClick(BaseEventData eventData)
    {
        OnPointerExit(null, false);
        
        if(activateSubMenu)
            UIManager.Instance.OpenedSubmenu(submenuAutoSelect.AutoSelect);
        else if (deactivateSubMenu)
            UIManager.Instance.CloseSubmenu();
        
        if (buttonType == ButtonType.Tween)
            tween.OnMouseClick();
        else if(buttonType == ButtonType.TweenDifficulty)
            tweenDifficulty.OnMouseClick();
        else {
            slotTween.OnClick();
            GetComponent<Button>().onClick.Invoke();
        }
    }

    public void OnPointerDown(BaseEventData eventData)
    {
        if (buttonType == ButtonType.Tween)
            tween.OnMouseDown();
        else if (buttonType == ButtonType.TweenDifficulty)
            tweenDifficulty.OnMouseDown();
    }

    public void OnPointerUp(BaseEventData eventData)
    {
        if (buttonType == ButtonType.Tween)
            tween.OnMouseUp();
        else if (buttonType == ButtonType.TweenDifficulty)
            tweenDifficulty.OnMouseUp();
    }
}
