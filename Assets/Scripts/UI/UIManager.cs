using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject activeButton;
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
    
    public void OnPointerEnter(GameObject button)
    {
        Debug.Log("OnPointerEnter " + button.name);
        activeButton = button;
        Debug.Log("New Active Button: " + button.name);
    }

    public void OnPointerExit(GameObject button)
    {
        Debug.Log("OnPointerExit " + button.name);
        activeButton = null;
    }

    public void ActivateActiveButton()
    {
       activeButton.GetComponent<UIButton>().OnPointerClick(null);
    }
    
    public void SelectButtonAbove()
    {
        if (activeButton == null) 
            return;
        
        var button = activeButton.GetComponent<UnityEngine.UI.Button>();
        var newActiveButton = button.navigation.selectOnUp;

        if (newActiveButton == null)
            return;
        
        activeButton.GetComponent<UIButton>().OnPointerExit(null); 
        newActiveButton.gameObject.GetComponent<UIButton>().OnPointerEnter(null); 
    }

    public void SelectButtonBelow()
    {
        if (activeButton == null)
            return;
        
        var button = activeButton.GetComponent<UnityEngine.UI.Button>();
        var newActiveButton = button.navigation.selectOnDown;

        if (newActiveButton == null)
            return;
        
        activeButton.GetComponent<UIButton>().OnPointerExit(null); 
        newActiveButton.gameObject.GetComponent<UIButton>().OnPointerEnter(null);

    }

    public void SelectButtonLeft()
    {
        if (activeButton == null)
            return;
        
        var button = activeButton.GetComponent<UnityEngine.UI.Button>();
        var newActiveButton = button.navigation.selectOnLeft;

        if (newActiveButton == null)
            return;
        
        activeButton.GetComponent<UIButton>().OnPointerExit(null);
        newActiveButton.gameObject.GetComponent<UIButton>().OnPointerEnter(null);
    }

    public void SelectButtonRight()
    {
        if (activeButton == null)
            return;
        
        var button = activeButton.GetComponent<UnityEngine.UI.Button>();
        var newActiveButton = button.navigation.selectOnRight;

        if (newActiveButton == null)
            return;
        
        activeButton.GetComponent<UIButton>().OnPointerExit(null); 
        newActiveButton.gameObject.GetComponent<UIButton>().OnPointerEnter(null);
    }


}
