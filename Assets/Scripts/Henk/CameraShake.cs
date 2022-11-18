using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraShake : MonoBehaviour
{
    private bool alreadyShaking = false;
    Vector3 originalPosition;

    public IEnumerator Shake(float duration, float magnitude)
    {
        if (alreadyShaking)
        {
            yield return null;
        }
        else
        {
            alreadyShaking = true;
            originalPosition = transform.localPosition;
            float elapsed = 0.0f;

            // Shake till the duration
            while (elapsed < duration)
            {
                float x = Random.Range(-1.0f, 1.0f) * magnitude;
                float y = Random.Range(-1.0f, 1.0f) * magnitude;

                transform.localPosition = new Vector3(x, y, originalPosition.z);

                elapsed += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }
            transform.localPosition = originalPosition;
            alreadyShaking = false;
        }
    }
}
