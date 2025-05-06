using UnityEngine;

public class ControllerAutoSelect : MonoBehaviour
{
    public GameObject AutoSelect; 

    void Start()
    {
        //activateButton();
    }

    public void Select()
    {
        //activateButton();
    }

    private void OnEnable()
    {
        //activateButton();
    }

    private void activateButton()
    {
        UIButton uiButton = AutoSelect.GetComponent<UIButton>();
        if (uiButton == null)
            return;
        
        uiButton.OnPointerEnter(null);
    }
}
