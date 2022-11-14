using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactTrigger : MonoBehaviour
{
    public float shakeDuration = 0.1f;
    public float shakePower = 0.2f;
    public CameraShake cameraShake;

    IEnumerator LerpColor(float time, float minValue, float maxValue, GameObject gameObject)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        float elapsed = 0.0f;
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float intensity = 0.0f;
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
            StartCoroutine(LerpColor(1f, 0.0f, 100.0f, this.gameObject));
            StartCoroutine(cameraShake.Shake(shakeDuration * 5.0f, shakePower * 2.0f));
        }
    }
}
