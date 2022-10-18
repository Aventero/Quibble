using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    public float JumpPower = 5.0f;

    [SerializeField] private bool isGrounded = false;
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigidbody.velocity = transform.up * JumpPower;
        }

        // check if apex is reached while releasing button
        if (Input.GetButtonUp("Jump") && Vector2.Dot(rigidbody.velocity, -transform.up) > 0.0f)
        {
        }

        // 
    }

    public bool IsGrounded()
    {
        return isGrounded = rigidbody.IsTouchingLayers(groundLayer);
    }
}
