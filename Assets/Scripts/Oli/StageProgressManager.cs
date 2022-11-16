using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageProgressManager : MonoBehaviour
{
    public Slider stageProgess;
    public float lerpTime = 0.2f;
    public float delay = 1f;

    private float lastValue = 0.0f;

    public void Setup()
    {
        // Get meteorites of current stage
        stageProgess.maxValue = GameManager.Instance.StageMeteoriteCount;

        // Reset progress
        stageProgess.value = 0;
        lastValue = 0.0f;
    }

    private void Update()
    {
        if (GameManager.Instance.MeteoritesHit != lastValue)
        {
            // Update slider
            StartCoroutine(UpdateProgress());
        }
    }

    IEnumerator UpdateProgress()
    {
        float deltaTime = 0.0f;
        while(deltaTime < lerpTime)
        {
            stageProgess.value = Mathf.Lerp(lastValue, GameManager.Instance.MeteoritesHit, deltaTime / lerpTime);
            deltaTime += Time.deltaTime;
            yield return null;
        }

        stageProgess.value = GameManager.Instance.MeteoritesHit;
        lastValue = GameManager.Instance.MeteoritesHit;
    }
}
