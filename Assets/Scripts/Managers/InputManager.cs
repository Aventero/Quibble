using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Vector2 MovementInput;
    public Vector2 Scrollvalue;
    public float Jump;
    public bool Attack;
    public bool Paused;

    public static event UnityAction OnPaused;

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jump = context.ReadValue<float>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Attack = context.ReadValueAsButton();
    }

    public void OnScrolling(InputAction.CallbackContext context)
    {
        Scrollvalue = context.ReadValue<Vector2>();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        // Button has reached the bottom of the key 
        if (context.started)
            OnPaused.Invoke();
        
        Paused = context.ReadValueAsButton();
    }

    public void DeviceChangeEvent(PlayerInput input)
    {
        switch (input.currentControlScheme)
        {
            case "Gamepad":
                Debug.Log("Using Gamepad");
                if(ControllerManager.Instance != null)
                    ControllerManager.Instance.ActivateGamepad();
                break;
            case "Keyboard&Mouse":
                Debug.Log("Using Keyboard & Mouse");
                if(ControllerManager.Instance != null)
                    ControllerManager.Instance.DeactivateGamepad();
                break;
        }
    }
}
