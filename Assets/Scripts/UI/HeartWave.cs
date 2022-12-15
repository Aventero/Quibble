using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartWave : MonoBehaviour
{
    public Material WaveMaterial;
    public float Duration = 0.2f;
    public float StartRadius = 0.2f;
    
    public void Beat()
    {
        StartCoroutine(HeartBeat(Duration));
    }

    public void StopBeating()
    {
        GetComponent<Animator>().enabled = false;
    }

    IEnumerator HeartBeat(float time)
    {
        float elapsed = StartRadius;
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            WaveMaterial.SetFloat("_WaveDistance", elapsed);
            WaveMaterial.SetFloat("_Fade", elapsed);
            yield return null;
        }

        // Hide the Wave
        WaveMaterial.SetFloat("_WaveDistance", 0);
        WaveMaterial.SetFloat("_Fade", 1);
    }
}
