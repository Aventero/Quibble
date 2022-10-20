using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    private Transform gravitationCenter;
    private Rigidbody2D rb2D;
    private float Acceleration = 5.0f;
    private Vector2 downVector;
    // Start is called before the first frame update
    void Start()
    {
        gravitationCenter = GameObject.FindGameObjectWithTag("Planet").transform;
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = new Vector2(Random.Range(-2, 2), Random.Range(-2, 2));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Calculate direction
        this.transform.up = -(gravitationCenter.position - this.transform.position);
        downVector = (gravitationCenter.transform.position - gameObject.transform.position).normalized;
        Debug.DrawRay(transform.position, downVector);
        Debug.DrawRay(transform.position, rb2D.velocity, Color.blue);
        rb2D.velocity += downVector * Time.fixedDeltaTime * Acceleration;

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
