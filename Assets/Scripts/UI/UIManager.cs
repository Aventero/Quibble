using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Stack<GameObject> activeButtons = new();
    private GameObject fallbackKeyboardButton;
    private static UIManager _instance;
    
    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject); // Destroy the entire GameObject, not just the component
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        ControllerManager.OnControllerActivated += () =>
        {
            Debug.LogWarning("UIManager: OnControllerActivated: " + activeButtons.Count.ToString());
            
            if (activeButtons.Count == 0)
                Debug.LogWarning("Activating: " + activeButtons.Peek().name);
;
            SelectActiveButton();
        };
    }

    public void OpenSubmenu(GameObject firstSelection)
    {
        if(ControllerManager.Instance.ActiveController())
            firstSelection.GetComponent<UIButton>().OnPointerEnter(null);
        else
            activeButtons.Push(firstSelection);
    }

    public void CloseSubmenu()
    {
        if(ControllerManager.Instance.ActiveController())
            activeButtons.Peek().GetComponent<UIButton>().OnPointerExit(null);
        else if(activeButtons.Count > 0)
            activeButtons.Pop();
        
        if (activeButtons.Count > 0)
           activeButtons.Peek().GetComponent<UIButton>().OnPointerEnter(null);
    }
    
    public void OnPointerEnter(GameObject button)
    {
        if(!ControllerManager.Instance.ActiveController())
            return;
            
        if (activeButtons.Count > 0)
            if (activeButtons.Peek().gameObject == button)
                return;
        
        activeButtons.Push(button);
    }

    public void OnPointerExit(GameObject button)
    {
        if(!ControllerManager.Instance.ActiveController())
            return;

        activeButtons.Pop();
    }

    public void ActivateActiveButton()
    {
        if (activeButtons.Count > 0)
            activeButtons.Peek().GetComponent<UIButton>().OnPointerClick(null);
        else
            Debug.LogError("<UIManager> ActivateActiveButton Count is ZERO");
    }

    public void SelectActiveButton() {
        if (activeButtons.Count > 0)
            activeButtons.Peek().GetComponent<UIButton>().OnPointerEnter(null);
        else
            Debug.LogError("<UIManager> SelectActiveButton Count is ZERO");
    }
    
    public void SelectButtonAbove()
    {
        if (activeButtons.Count == 0) 
            return;

        var activeButtonGameObject = activeButtons.Peek();

        if (activeButtonGameObject.GetComponent<UIDropDown>() != null)
            if (activeButtonGameObject.GetComponent<UIDropDown>().IsDropdownOpen())
                return;
        
        var navigation = GetNavigation(activeButtonGameObject);
        var newActiveButton = navigation.selectOnUp;

        if (newActiveButton == null)
            return;
        
        activeButtonGameObject.GetComponent<UIButton>().OnPointerExit(null); 
        newActiveButton.gameObject.GetComponent<UIButton>().OnPointerEnter(null);
    }

    public void SelectButtonBelow()
    {
        if (activeButtons.Count == 0)
            return;
        
        var activeButtonGameObject = activeButtons.Peek();
        
        if (activeButtonGameObject.GetComponent<UIDropDown>() != null)
            if (activeButtonGameObject.GetComponent<UIDropDown>().IsDropdownOpen())
                return;
        
        var navigation = GetNavigation(activeButtonGameObject);
        var newActiveButton = navigation.selectOnDown;
        
        if (newActiveButton == null)
            return;
        
        activeButtonGameObject.GetComponent<UIButton>().OnPointerExit(null); 
        newActiveButton.gameObject.GetComponent<UIButton>().OnPointerEnter(null);
    }

    public void SelectButtonLeft()
    {
        if (activeButtons.Count == 0)
            return;

        var activeButtonGameObject = activeButtons.Peek();
        if (activeButtonGameObject.GetComponent<UISlider>() != null){
            activeButtonGameObject.GetComponent<UISlider>().MoveSlider(-1);
            return;
        }

        var navigation = GetNavigation(activeButtonGameObject);
        var newActiveButton = navigation.selectOnLeft;

        if (newActiveButton == null)
            return;
        
        activeButtonGameObject.GetComponent<UIButton>().OnPointerExit(null);
        newActiveButton.gameObject.GetComponent<UIButton>().OnPointerEnter(null);
    }

    public void SelectButtonRight()
    {
        if (activeButtons.Count == 0)
            return;
        
        var activeButtonGameObject = activeButtons.Peek();
        if (activeButtonGameObject.GetComponent<UISlider>() != null) {
            activeButtonGameObject.GetComponent<UISlider>().MoveSlider(1);
            return;
        }

        var navigation = GetNavigation(activeButtonGameObject);
        var newActiveButton = navigation.selectOnRight;

        if (newActiveButton == null)
            return;
        
        activeButtonGameObject.GetComponent<UIButton>().OnPointerExit(null); 
        newActiveButton.gameObject.GetComponent<UIButton>().OnPointerEnter(null);
    }

    private Navigation GetNavigation(GameObject button)
    {
        if (button.GetComponent<Slider>() != null)
            return button.GetComponent<Slider>().navigation;
        if (button.GetComponent<TMP_Dropdown>() != null)
            return button.GetComponent<TMP_Dropdown>().navigation;
        if (button.GetComponent<Button>() != null)
            return button.GetComponent<Button>().navigation;
        
        Debug.LogError("No Navigation Found");
        return Navigation.defaultNavigation;
    }
}
