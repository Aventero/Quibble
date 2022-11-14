using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    public Animator transititon;
    public float transitionTime = 1f;

    public void LoadGameScene()
    {
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        transititon.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
