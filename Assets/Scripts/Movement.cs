using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float JumpPower = 16f;

    public float horizontal;
    public float speed = 8f;
    public bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, JumpPower);
        }

        if (Input.GetButtonUp("Jump") && rigidbody2D.velocity.y > 0f)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * 0.5f);
        }

        CheckForFlip();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(horizontal * speed, rigidbody2D.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void CheckForFlip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f; // flip player 
            transform.localScale = localScale;
        }
    }
}
