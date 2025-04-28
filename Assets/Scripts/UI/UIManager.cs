using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Stack<GameObject> activeButtons = new();
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

        ControllerManager.OnControllerActivated += () =>
        {
            if (activeButtons.Count == 0)
                return;
            
            // TODO: Check if this is even called when the controller is being used -> Switch from keyboard to controller
            // TODO: Show the active Button
            //activeButtons.Peek().GetComponent<UIButton>().OnPointerEnter(null);
        };
    }

    public void OpenedSubmenu(GameObject firstSelection)
    {
        firstSelection.GetComponent<UIButton>().OnPointerEnter(null);
    }

    public void CloseSubmenu()
    {
        activeButtons.Peek().GetComponent<UIButton>().OnPointerExit(null);
        
        if (activeButtons.Count > 0)
           activeButtons.Peek().GetComponent<UIButton>().OnPointerEnter(null);
    }
    
    public void OnPointerEnter(GameObject button)
    {
        if (activeButtons.Count > 0)
            if (activeButtons.Peek().gameObject == button)
                return;
        
        activeButtons.Push(button);
    }

    public void OnPointerExit(GameObject button)
    {
        activeButtons.Pop();
    }

    public void ActivateActiveButton()
    {
       activeButtons.Peek().GetComponent<UIButton>().OnPointerClick(null);
    }

    public void SelectActiveButton() {
        activeButtons.Peek().GetComponent<UIButton>().OnPointerEnter(null);
    }
    
    public void SelectButtonAbove()
    {
        if (activeButtons.Count == 0) 
            return;
        
        var button = activeButtons.Peek().GetComponent<Button>();
        var newActiveButton = button.navigation.selectOnUp;

        if (newActiveButton == null)
            return;
        
        button.GetComponent<UIButton>().OnPointerExit(null); 
        newActiveButton.gameObject.GetComponent<UIButton>().OnPointerEnter(null);
    }

    public void SelectButtonBelow()
    {
        if (activeButtons.Count == 0)
            return;
        
        var button = activeButtons.Peek().GetComponent<Button>();
        var newActiveButton = button.navigation.selectOnDown;

        if (newActiveButton == null)
            return;
        
        button.GetComponent<UIButton>().OnPointerExit(null); 
        newActiveButton.gameObject.GetComponent<UIButton>().OnPointerEnter(null);
    }

    public void SelectButtonLeft()
    {
        if (activeButtons.Count == 0)
            return;

        var button = activeButtons.Peek().GetComponent<Button>();
        var newActiveButton = button.navigation.selectOnLeft;

        if (newActiveButton == null)
            return;
        
        button.GetComponent<UIButton>().OnPointerExit(null);
        newActiveButton.gameObject.GetComponent<UIButton>().OnPointerEnter(null);
    }

    public void SelectButtonRight()
    {
        if (activeButtons.Count == 0)
            return;
        
        var button = activeButtons.Peek().GetComponent<Button>();
        var newActiveButton = button.navigation.selectOnRight;

        if (newActiveButton == null)
            return;
        
        button.GetComponent<UIButton>().OnPointerExit(null); 
        newActiveButton.gameObject.GetComponent<UIButton>().OnPointerEnter(null);
    }
}
