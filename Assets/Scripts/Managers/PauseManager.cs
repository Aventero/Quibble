using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public PlayerInput PlayerInput;
    public GameObject Player;
    public GameObject PauseMenu;
    public GameObject AutoSelect; 
    public bool IsAlreadyPaused = false;

    private void Start()
    {
        InputManager.OnPaused += EnablePauseMenu;
        HealthManager.OnDeath += Disable;
        
        PlayerInput.SwitchCurrentActionMap("Player");
    }

    private void EnablePauseMenu()
    {
        if (IsAlreadyPaused)
        {
            DisablePauseMenu();
            return;
        }

        Player.GetComponent<PlayerController>().enabled = false;
        StateManager.InMenu = true;
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsAlreadyPaused = true;
        
        PlayerInput.SwitchCurrentActionMap("UI");
        
        // Auto select if a controller is used
        if(ControllerManager.Instance.ActiveController())
            AutoSelect.GetComponent<UIButton>().OnPointerEnter(null);
            
    }

    public void DisablePauseMenu()
    {
        Player.GetComponent<PlayerController>().enabled = true;
        
        PlayerInput.SwitchCurrentActionMap("Player");
        
        Time.timeScale = 1f;
        IsAlreadyPaused = false;
        PauseMenu.SetActive(false);
        StateManager.InMenu = false;
    }

    public void Disable()
    {
        GameManager.Instance.Player.GetComponent<PlayerController>().PlayerControls.FindAction("Pause").Disable();
    }

    private void OnDestroy()
    {
        InputManager.OnPaused -= EnablePauseMenu;
    }
}
