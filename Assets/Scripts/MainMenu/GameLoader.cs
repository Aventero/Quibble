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

    public void Start()
    {
        SaveManager.Initialize();
    }

    public void LoadGameScene()
    {
        SaveManager.Load();
        StartCoroutine(LoadGame());
    }

    public void LoadMainMenuScene()
    {
        SaveManager.Load();

        // Reset all events
        ExplanationManager.ResetTrigger();
        Meteorite.ResetOnPlanetHit();
        Sword.ResetOnMeteoriteHit();

        StartCoroutine(LoadMainMenu());
    }

    public void LoadTutorialScene()
    {
        StartCoroutine(LoadTutorial());
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

    IEnumerator LoadTutorial()
    {
        Time.timeScale = 1f;
        transititon.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(2);
        StateManager.Init();
    }
}
