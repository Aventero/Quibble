using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactTrigger : MonoBehaviour
{
    public float shakeDuration = 0.1f;
    public float shakePower = 0.2f;
    public CameraShake cameraShake;
    public GameObject trigger;

    IEnumerator LerpColor(float time, float minValue, float maxValue, GameObject gameObject)
    {
        Renderer renderer = gameObject.transform.GetComponent<Renderer>();
        float elapsed = 0.0f;
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float intensity;
            if (elapsed / time <= 0.5f)
                intensity = Mathf.Lerp(minValue, maxValue, elapsed / time);
            else
            {
                intensity = Mathf.Lerp(maxValue, minValue, elapsed / time);
            }
            renderer.material.SetFloat("_Intensity", intensity);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Meteorite"))
        {
            StartCoroutine(LerpColor(0.5f, 0.0f, 50.0f, trigger));
            StartCoroutine(cameraShake.Shake(shakeDuration * 2.0f, shakePower * 2.0f));
        }
    }
}
