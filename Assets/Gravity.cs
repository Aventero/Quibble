using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float acceleration = 1f;
    public float mass = 1f;

    private Rigidbody2D rigid;
    private Transform gravitationCenter;

    public Vector2 downVector;
    public Vector2 downVectorNormalized;

    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        gravitationCenter = GameObject.FindGameObjectWithTag("Planet").transform;
    }

    public void FixedUpdate()
    {
        // Calc direction
        downVector = (gravitationCenter.transform.position - gameObject.transform.position);
        downVectorNormalized = downVector.normalized;

        // Apply force
        rigid.velocity += downVector * Time.deltaTime * acceleration * mass;
    }
}
