using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    private bool coroutineIsRunning = false;
    private float defaultTimeScale = 1f;
    public AnimationCurve TimeScaling;

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
        float elapsed = 0f;
        duration += 0.000001f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            // Time.timeScale = Mathf.Lerp(0f, defaultTimeScale, elapsed / duration);
            Time.timeScale = TimeScaling.Evaluate(elapsed / duration);
            yield return null;
        }
        Time.timeScale = defaultTimeScale;
        coroutineIsRunning = false;
    }
}
