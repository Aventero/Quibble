using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class TutorialMeteorite : MonoBehaviour
{
    [Header("Meteorite")]
    public AnimationCurve SpawnRadiusByStage;
    public float MeteoriteSpeed = 1.0f;
    public float SlappedSpeed = 2.0f;
    public float Gravity = 2.0f;
    public float SlappedGravity = 4.0f;
    public float MinScale = 0.1f;
    public float ScaleByDistance = 10.0f;
    public float EllipseWidth = 1.0f;
    public float EllipseHeight = 1.0f;
    public float DeletionRadius = 75.0f;
    public float damage = 1.0f;
    private float currentRadius;

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
        if (currentRadius <= 0 || currentRadius >= DeletionRadius)
            Destroy(gameObject);
    }

    private void SetStartingPosition()
    {
        AngleRad = Random.Range(0f, 2f * Mathf.PI);
        currentRadius = Random.Range(SpawnRadiusByStage.Evaluate(GameManager.Instance.CurrentStage) - 2f, SpawnRadiusByStage.Evaluate(GameManager.Instance.CurrentStage) + 2f);
        UpdatePosition();
    }

    private void UpdateSpeed()
    {
        AngleRad += MeteoriteSpeed * Time.deltaTime;
        float distance = Vector3.Distance(transform.position, gravitationCenter.position);
        float fallingMultiplier = GameManager.Instance.GetFallingCurve(1f - ((distance) / SpawnRadiusByStage.Evaluate(GameManager.Instance.CurrentStage)));
        currentRadius += -Gravity * Time.deltaTime * fallingMultiplier;
    }

    private void UpdatePosition()
    {
        float x = EllipseWidth * currentRadius * Mathf.Cos(AngleRad);
        float y = EllipseHeight * currentRadius * Mathf.Sin(AngleRad);
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
            Gravity = -SlappedGravity; // Negate gravity, so the meteorite shoots away
            MeteoriteSpeed = SlappedSpeed;

            // After hitting, change the trail color
            FindObjectOfType<AudioManager>().Play("Hit");
            ChangeTrail(Color.green);
            StartCoroutine(cameraShake.Shake(ShakeDuration, ShakePower));
            GetComponent<Collider2D>().enabled = false;
        }

        if (collision.transform.CompareTag("Planet"))
        {
            FindObjectOfType<AudioManager>().Play("PlanetHit");
            // Deal damage
            HealthManager.Instance.DealDamage(damage);
            GetComponent<Collider2D>().enabled = false;
            OnPlanetHit.Invoke();

            StartCoroutine(FadeTrail(ShakeDuration * 2.0f, this.gameObject));
            StartCoroutine(DestoryAfter(ShakeDuration * 2.0f, this.gameObject));
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
