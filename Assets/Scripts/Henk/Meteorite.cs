using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Meteorite : MonoBehaviour
{
    public float ScalingFactor = 1.0f;
    private Transform gravitationCenter;
    public float radius = 5;
    public float angle = 0;
    public float speed = 1.0f;
    public float gravity = 1.0f;
    public float minimumScale = 0.1f;
    public float scalingByDistance = 10.0f;
    public float width = 1.0f;
    public float height = 1.0f;
    public float deletionRadius = 20.0f;
    public float shakeDuration = 0.1f;
    public float shakePower = 0.2f;
    private CameraShake cameraShake;
    public static event UnityAction OnPlanetHit;

    public float AngleRad
    {
        get { return angleRad; }
        set
        {
            angleRad = (AngleRad < 0) ? 2 * Mathf.PI : value;
            angleRad = (AngleRad > 2 * Mathf.PI) ? 0 : value;
        }
    }

    private float angleRad = 0;

    // Start is called before the first frame update
    void Start()
    {
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        gravitationCenter = GameObject.FindGameObjectWithTag("Planet").transform;
        SetStartingPosition();
        ScaleMeteorite();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateSpeed();
        UpdatePosition();
        ScaleMeteorite();
        UpdateRotation();
        LookForDeletion();
    }

    private void UpdateRotation()
    {
        transform.localRotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angleRad);
    }

    private void LookForDeletion()
    {
        if (radius <= 0 || radius >= deletionRadius)
            Destroy(gameObject);
    }

    private void SetStartingPosition()
    {
        AngleRad = Random.Range(0, 2 * Mathf.PI);
        radius = Random.Range(7.0f, 9.0f);
        UpdatePosition();
    }

    private void UpdateSpeed()
    {
        AngleRad += 1.0f * speed * Time.deltaTime;
        radius += -0.1f * gravity * Time.deltaTime;
    }

    private void UpdatePosition()
    {
        float x = width * radius * Mathf.Cos(AngleRad);
        float y = height * radius * Mathf.Sin(AngleRad);
        transform.position = new Vector2(x, y);
    }

    private void ScaleMeteorite()
    {
        // Scale the meteorite by distance
        float distance = Vector3.Distance(transform.position, gravitationCenter.position);
        transform.localScale = Vector3.one * ((scalingByDistance / (Mathf.Pow(distance, 2) + scalingByDistance)) + minimumScale);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Sword"))
        {
            gravity = -50.0f; // Negate gravity, so the meteorite shoots away
            speed *= 2.0f;

            // After hitting, change the trail color
            ChangeTrail(Color.green);
            StartCoroutine(cameraShake.Shake(shakeDuration, shakePower));
            GetComponent<Collider2D>().enabled = false;
        }

        if (collision.transform.CompareTag("Planet"))
        {
            StartCoroutine(FadeTrail(shakeDuration * 2.0f, this.gameObject));
            StartCoroutine(DestoryAfter(shakeDuration * 2.0f, this.gameObject));
            OnPlanetHit.Invoke();
            GetComponent<Collider2D>().enabled = false;
        }
    }

    private void ChangeTrail(Color color)
    {
        TrailRenderer trailRenderer = gameObject.GetComponent<TrailRenderer>();
        trailRenderer.startColor = color;
        trailRenderer.endColor = color;
    }

    IEnumerator FadeTrail(float time, GameObject gameObject)
    {
        time += 0.000001f;  // just to make sure its not 0
        float elapsed = 0.0f;
        TrailRenderer trailRenderer = gameObject.GetComponent<TrailRenderer>();
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            trailRenderer.startColor = new Color(trailRenderer.startColor.r, trailRenderer.startColor.g, trailRenderer.startColor.b, 1 - (elapsed / time));
            trailRenderer.endColor = new Color(trailRenderer.endColor.r, trailRenderer.endColor.g, trailRenderer.endColor.b, 1 - (elapsed / time));
            yield return null;
        }
    }

    public IEnumerator DestoryAfter(float time, GameObject gameObject)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
