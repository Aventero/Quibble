using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    public float MaxJumpPower = 5.0f;
    public float ForwardPower = 5.0f;
    public float JumpLoadingSpeed = 5.0f;
    public float JumpPower = 0f;
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
        MaxJumpPower = Scrolling.CenterDistance + 5.0f;
        ForwardPower = Scrolling.CenterDistance + 5.0f;

        if (Input.GetButton("Jump"))
        {
            JumpPower += (Scrolling.CenterDistance + JumpLoadingSpeed) * Time.deltaTime;

            if (JumpPower > MaxJumpPower)
                JumpPower = MaxJumpPower;
        }

        // Jump
        if (Input.GetButtonUp("Jump"))
        {
            rigidbody.velocity = transform.up * (JumpPower + Scrolling.CenterDistance);
            JumpPower = 0;
        }

        //// check if apex is reached while releasing button
        //if (Input.GetButtonUp("Jump") && Vector2.Dot(rigidbody.velocity, -transform.up) > 0.0f)
        //{
        //}
    }

    private void FixedUpdate()
    {
        if (!IsGrounded())
        {
            rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, transform.right * (ForwardPower + Scrolling.CenterDistance), Time.fixedDeltaTime);
        }
    }

    public bool IsGrounded()
    {
        return isGrounded = rigidbody.IsTouchingLayers(groundLayer);
    }
}
