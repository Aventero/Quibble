using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float acceleration = 10f;
    public float drag = 0.95f;

    public Transform gravitationCenter;
    private Rigidbody2D rigid;

    private Vector2 downVector;
    private Vector2 downVectorNormalized;

    public Vector2 DownVector => downVector;
    public Vector2 UpVector => -downVector;
    public Vector2 DownVectorNormalized => downVector.normalized;
    public Vector2 ForwardVector => new Vector2(downVector.y, -downVector.x);

    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        gravitationCenter = GameObject.FindGameObjectWithTag("Planet").transform;
    }

    public void Update()
    {
    }

    public void FixedUpdate()
    {
        // Calculate direction
        this.transform.up = -(gravitationCenter.position - this.transform.position);
        downVector = (gravitationCenter.transform.position - gameObject.transform.position);
        downVectorNormalized = downVector.normalized;
        Debug.DrawRay(transform.position, downVector);
        Debug.DrawRay(transform.position, rigid.velocity, Color.blue);
        rigid.velocity += downVector * Time.fixedDeltaTime * acceleration;
        rigid.velocity *= drag;
    }
}
