using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuManager : MonoBehaviour
{
    public GameLoader gameLoader;
    public GameObject Player;

    private void Start()
    {
        Player.GetComponent<PlayerController>().PlayerControls.FindAction("Pause").Disable();
    }

    public void StartGame()
    {
        Player.GetComponent<PlayerController>().PlayerControls.FindAction("Pause").Enable();
        gameLoader.LoadGameScene();
    }

    public void LoadTutorial()
    {
        Player.GetComponent<PlayerController>().PlayerControls.FindAction("Pause").Enable();
        gameLoader.LoadTutorialScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
