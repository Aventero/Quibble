using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    private bool coroutineIsRunning = false;
    private float defaultTimeScale = 1f;

    public void Start()
    {
        defaultTimeScale = Time.timeScale;
    }

    public void Update()
    {
    }

    public void Stop(float duration)
    {
        if (coroutineIsRunning)
            return;

        if (!coroutineIsRunning)
        {
            coroutineIsRunning = true;
            Time.timeScale = 0f;
            StartCoroutine(Stopping(duration));
        }
    }

    IEnumerator Stopping(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = defaultTimeScale;
        coroutineIsRunning = false;
    }
}
