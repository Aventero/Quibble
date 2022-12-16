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
        Player.GetComponent<PlayerInput>().currentActionMap.FindAction("Pause").Disable();
    }

    public void StartGame()
    {
        Player.GetComponent<PlayerInput>().currentActionMap.FindAction("Pause").Enable();
        gameLoader.LoadGameScene();
    }

    public void LoadTutorial()
    {
        Player.GetComponent<PlayerInput>().currentActionMap.FindAction("Pause").Enable();
        gameLoader.LoadTutorialScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
