using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float JumpPower = 5.0f;
    public float MaxJumpPower = 5.0f;
    public float ForwardSpeed = 4f;

    public bool isGrounded = false;
    [SerializeField] private new Rigidbody2D rigidbody2D;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
    }

    // Start is called before the first frame update
    private void Update()
    {
        // hold jump
        //if (Input.GetButton("Jump") && IsGrounded() && jumpStrength < MaxJumpPower)
        //{
        //    jumpStrength += JumpPower * Time.deltaTime;
        //    if (jumpStrength > MaxJumpPower)
        //        jumpStrength = MaxJumpPower;
        //}

        // Jump 
        //if (Input.GetButtonUp("Jump") && IsGrounded())
        //{
        //    rigidbody2D.velocity = transform.up * JumpPower;
        //    jumpStrength = 0; // Reset jumpStrenght
        //}

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigidbody2D.velocity = transform.up * JumpPower;
        }

        if (Input.GetButtonUp("Jump") && Vector2.Dot(rigidbody2D.velocity, transform.up) > 0.0f)
        {
            Debug.Log("Jump released while not falling");
            Debug.Log(Vector2.Dot(rigidbody2D.velocity.normalized, transform.up.normalized));
            Debug.Log(Mathf.Lerp(0.5f, 1f, Vector2.Dot(rigidbody2D.velocity.normalized, transform.up.normalized)));
            rigidbody2D.velocity -= new Vector2(transform.up.x, transform.up.y) * Mathf.Lerp(0.5f, 1f, Vector2.Dot(rigidbody2D.velocity.normalized, transform.up.normalized));
            // Slow the upwards momentum?
            //rigidbody2D.velocity -= new Vector2(transform.up.x, transform.up.y) * Vector2.Dot(rigidbody2D.velocity, transform.up);
            //rigidbody2D.velocity *= 0.5f;
        }
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = Vector2.Lerp(rigidbody2D.velocity, transform.right * ForwardSpeed, Time.fixedDeltaTime);
    }

    public bool IsGrounded()
    {
        return isGrounded = rigidbody2D.IsTouchingLayers(groundLayer);
    }


    private void OnDrawGizmos()
    {
        //if (collider != null)
        //    Gizmos.DrawWireCube(collider.bounds.center + (-transform.up * 0.1f), transform.localScale);
    }
}
