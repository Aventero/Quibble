using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameLoader gameLoader;

    public void StartGame()
    {
        gameLoader.LoadGameScene();
    }

    public void OpenSettings()
    {
        gameLoader.LoadTutorialScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}