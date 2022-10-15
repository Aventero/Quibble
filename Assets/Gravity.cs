using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float acceleration = 10f;

    private Rigidbody2D rigid;
    private Transform gravitationCenter;

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

    public void FixedUpdate()
    {
        // Calculate direction
        downVector = (gravitationCenter.transform.position - gameObject.transform.position);
        downVectorNormalized = downVector.normalized;

        // Apply force
        rigid.velocity += downVectorNormalized * Time.fixedDeltaTime * acceleration;

        Debug.DrawRay(transform.position, downVectorNormalized);
    }
}
