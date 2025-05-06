using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputManager : MonoBehaviour
{
    public InputManager inputManager;
    
    private bool paused;
    private bool stickMovement;
    
    private void Start()
    {
        InputManager.OnPaused += OnPaused;
    }
    
    public void NavigateUI(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        if(moveInput.x == 0 && moveInput.y == 0) {
            stickMovement = false;
            return;
        }

        if(stickMovement)
            return;

        if (moveInput.y == 1){
            UIManager.Instance.SelectButtonAbove();
            stickMovement = true;
        } else if(moveInput.y == -1){
            UIManager.Instance.SelectButtonBelow();
            stickMovement = true;
        }

        if(moveInput.x == 1) {
            UIManager.Instance.SelectButtonRight();
            stickMovement = true;
        } else if(moveInput.x == -1) {
            UIManager.Instance.SelectButtonLeft();
            stickMovement = true;
        }
        
    }

    public void Up(InputAction.CallbackContext context)
    {
        // Not a complete button press
        if (!context.started)
            return;
        
        Debug.Log("UP");
        UIManager.Instance.SelectButtonAbove();
    }

    public void Down(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        Debug.Log("Down");
        UIManager.Instance.SelectButtonBelow();
    }

    public void Left(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        Debug.Log("Left");
        UIManager.Instance.SelectButtonLeft();
    }

    public void Right(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        
        Debug.Log("Right");
        UIManager.Instance.SelectButtonRight();
    }

    public void Action(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        
        Debug.Log("Action");
        UIManager.Instance.ActivateActiveButton();
    }
    
    public void Back(InputAction.CallbackContext context)
    {
        Debug.Log("Back");
        if (paused)
        {
            inputManager.OnPause(context);
        }
    }

    private void OnPaused()
    {
        paused = !paused;
    }
}
