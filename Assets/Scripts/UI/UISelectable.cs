using UnityEngine;

public abstract class UISelectable : MonoBehaviour {
    public abstract void OnPointerEnter();
    public abstract void OnPointerExit();
    public abstract void OnPointerClick();
    public abstract void OnPointerUp();
    public abstract void OnPointerDown();
}