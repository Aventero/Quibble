using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenu;
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

        StateManager.InMenu = true;
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsAlreadyPaused = true;
    }

    public void DisablePauseMenu()
    {
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
