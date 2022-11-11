using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        radius = Random.Range(7, 9);
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
        Debug.Log("Trigger");
        if (collision.transform.CompareTag("Player"))
        {
            gravity = -50.0f; // Negate gravity, so the meteorite shoots away
        }
    }
}
