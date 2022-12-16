using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject PauseMenu;
    public Button AutoSelect; 
    public bool IsAlreadyPaused = false;

    private void Start()
    {
        InputManager.OnPaused += EnablePauseMenu;
        HealthManager.OnDeath += Disable;
    }

    private void EnablePauseMenu()
    {
        if (IsAlreadyPaused)
        {
            DisablePauseMenu();
            return;
        }

        // Select first ui element if controller is connected
        if (Gamepad.all.Count > 0)
            AutoSelect.Select();

        Player.GetComponent<PlayerController>().enabled = false;
        StateManager.InMenu = true;
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsAlreadyPaused = true;
    }

    public void DisablePauseMenu()
    {
        Player.GetComponent<PlayerController>().enabled = true;
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
