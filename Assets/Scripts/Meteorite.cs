using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public float ScalingFactor = 1.0f;
    public float StartPower = 10.0f;
    public float acceleration = 5.0f;
    public float drag = 0.95f;
    private Transform gravitationCenter;
    private Rigidbody2D rb2D;
    private Vector2 downVector;
    
    // Start is called before the first frame update
    void Start()
    {
        gravitationCenter = GameObject.FindGameObjectWithTag("Planet").transform;
        rb2D = GetComponent<Rigidbody2D>();
        transform.up = -(gravitationCenter.position - transform.position);

        rb2D.velocity = transform.right * StartPower;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Calculate direction
        this.transform.up = -(gravitationCenter.position - transform.position);
        downVector = (gravitationCenter.transform.position - gameObject.transform.position).normalized;
        Debug.DrawRay(transform.position, downVector);
        Debug.DrawRay(transform.position, rb2D.velocity, Color.blue);

        // Gravitation
        rb2D.velocity += downVector * Time.fixedDeltaTime * acceleration;
        rb2D.velocity *= drag;

        // Scale the meteorite by distance
        float distance = Vector3.Distance(transform.position, gravitationCenter.position);
        transform.localScale = Vector3.one * 1.0f / (distance + 1) * ScalingFactor;

        // Lookat in 2D on pivot
        transform.up = -(gravitationCenter.position - transform.position);

        if (Vector2.Distance(gravitationCenter.position, transform.position) >= 10.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            rb2D.bodyType = RigidbodyType2D.Kinematic;
            rb2D.velocity = transform.up * 5.0f * Random.Range(0, 10);
        }
    }
}
