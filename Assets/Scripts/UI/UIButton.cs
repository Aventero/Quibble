using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour
{
    private Tween tween;

    private void Awake()
    {
        tween = GetComponent<Tween>();
    }

    public void OnPointerEnter(BaseEventData eventData)
    {
        UIManager.Instance.OnPointerEnter(gameObject);
        tween.OnMouseEnter();
    }

    public void OnPointerExit(BaseEventData eventData)
    {
        UIManager.Instance.OnPointerExit(gameObject);
        tween.OnMouseExit();
    }

    public void OnPointerClick(BaseEventData eventData)
    {
        
        tween.OnMouseClick();
    }

    public void OnPointerDown(BaseEventData eventData)
    {
        tween.OnMouseDown();
    }

    public void OnPointerUp(BaseEventData eventData)
    {
        tween.OnMouseUp();
    }

}
