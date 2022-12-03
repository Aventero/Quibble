using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class Meteorite : MonoBehaviour
{
    [Header("")]
    public float SpawnRadius = 5;
    public float MeteoriteSpeed = 1.0f;
    public float Gravity = 1.0f;
    public float MinScale = 0.1f;
    public float ScaleByDistance = 10.0f;
    public float EllipseWidth = 1.0f;
    public float EllipseHeight = 1.0f;
    public float DeletionRadius = 20.0f;
    public float damage = 1.0f;

    [Header("Camera Shake")]
    public float ShakeDuration = 0.1f;
    public float ShakePower = 0.2f;
    public static event UnityAction OnPlanetHit;
    
    private CameraShake cameraShake;
    private Transform gravitationCenter;

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
        if (SpawnRadius <= 0 || SpawnRadius >= DeletionRadius)
            Destroy(gameObject);
    }

    private void SetStartingPosition()
    {
        AngleRad = Random.Range(0, 2 * Mathf.PI);
        SpawnRadius = Random.Range(7.0f, 9.0f);
        UpdatePosition();
    }

    private void UpdateSpeed()
    {
        AngleRad += PlayerStats.Instance.Slow * MeteoriteSpeed * Time.deltaTime;
        SpawnRadius += -0.1f * Gravity * Time.deltaTime;
    }

    private void UpdatePosition()
    {
        float x = EllipseWidth * SpawnRadius * Mathf.Cos(AngleRad);
        float y = EllipseHeight * SpawnRadius * Mathf.Sin(AngleRad);
        transform.position = new Vector2(x, y);
    }

    private void ScaleMeteorite()
    {
        // Scale the meteorite by distance
        float distance = Vector3.Distance(transform.position, gravitationCenter.position);
        transform.localScale = Vector3.one * ((ScaleByDistance / (Mathf.Pow(distance, 2) + ScaleByDistance)) + MinScale);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Sword"))
        {
            Gravity = -50.0f; // Negate gravity, so the meteorite shoots away
            MeteoriteSpeed *= 2.0f;

            // After hitting, change the trail color
            FindObjectOfType<AudioManager>().Play("Hit");
            ChangeTrail(Color.green);
            StartCoroutine(cameraShake.Shake(ShakeDuration, ShakePower));
            GetComponent<Collider2D>().enabled = false;
        }

        if (collision.transform.CompareTag("Planet"))
        {
            FindObjectOfType<AudioManager>().Play("PlanetHit");

            StartCoroutine(FadeTrail(ShakeDuration * 2.0f, this.gameObject));
            StartCoroutine(DestoryAfter(ShakeDuration * 2.0f, this.gameObject));
            OnPlanetHit.Invoke();
            GetComponent<Collider2D>().enabled = false;

            // Deal damage
            HealthManager.Instance.DealDamage(damage);
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
