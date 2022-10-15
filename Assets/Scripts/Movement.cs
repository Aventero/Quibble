using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float JumpPower = 2;

    public float horizontal;
    public float forwardSpeed = 3f;
    public bool isFacingRight = true;
    public bool isGrounded = false;

    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private BoxCollider2D collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    private void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigidbody2D.velocity = JumpPower * transform.right;
        }

        //if (Input.GetButtonUp("Jump") && rigidbody2D.velocity.y > 0f)
        //{
        //    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * 0.5f);
        //}

  
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!IsGrounded())
        {
            Debug.Log("Not Grounded");
            Debug.DrawRay(transform.position, -transform.up, Color.red);
            rigidbody2D.AddForce(-transform.up * Time.fixedDeltaTime * forwardSpeed);
        }
        //rigidbody2D.velocity = horizontal * speed * GetComponent<Gravity>().ForwardVector ;
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(collider.bounds.center, transform.localScale, 0f, transform.right * -1f, 0.1f, groundLayer);
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

    private void OnDrawGizmos()
    {
        if (collider != null)
        {
            Gizmos.DrawWireCube(collider.bounds.center + ((transform.right * -1f) * 0.1f), transform.localScale);
        }
    }
}
