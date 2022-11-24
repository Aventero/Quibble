using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    public Animator transititon;
    public float transitionTime = 1f;
    public Slider slider;

    public void LoadGameScene()
    {
        StartCoroutine(LoadGame());
    }

    public void LoadMainMenuScene()
    {
        StartCoroutine(LoadMainMenu());
    }

    IEnumerator LoadGame()
    {
        Time.timeScale = 1f;
        transititon.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(1);
        StateManager.Init();
    }   

    IEnumerator LoadMainMenu()
    {
        Time.timeScale = 1f;
        transititon.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(0);
        StateManager.Init();
    }
}
