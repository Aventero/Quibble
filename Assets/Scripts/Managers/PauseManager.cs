using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public InputManager InputManager;
    public GameObject PauseMenu;
    public bool IsAlreadyPaused = false;

    private void Start()
    {
        InputManager.OnPaused += EnablePauseMenu;
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

    private void OnDestroy()
    {
        InputManager.OnPaused -= EnablePauseMenu;
    }
}