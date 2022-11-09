using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float minHeight = 5;

    [Header("Movement Settings")]
    public float movementSpeed = 1;
    public float maxJumpTime;
    public float maxJumpHeight;
    public float fallMultiplier;

    private InputManager inputManager;
    private float velocity;
    private float currentHeight;
    private float gravity;
    private float initialJumpVelocity;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        currentHeight = minHeight;

        // Setup jump
        SetupJumpVariables();
    }

    private void Update()
    {
        // Movement & Rotation
        HandleMovement();

        // Apply velocity
        currentHeight += velocity * Time.deltaTime;

        CheckGrounded();
        HandleGravity();
        HandleJump();

        // Polar -> Cartesian
        float xCoord = currentHeight * Mathf.Cos(StateManager.AngleRad);
        float yCoord = currentHeight * Mathf.Sin(StateManager.AngleRad);
        transform.position = new Vector2 (xCoord, yCoord);
    }

    void HandleMovement()
    {
        // Calculate new angles
        StateManager.AngleRad -= inputManager.MovementInput.x * movementSpeed * Time.deltaTime;

        // Rotate player
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, StateManager.AngleDeg - 90f));
    }

    void CheckGrounded()
    {
        if (currentHeight <= minHeight)
        {
            currentHeight = minHeight;
            StateManager.IsGrounded = true;
        }
        else
        {
            StateManager.IsGrounded = false;
        }
    }

    void HandleGravity()
    {
        // Check if player is currently falling
        bool isFalling = velocity <= 0.0f || !(inputManager.Jump > 0.0);

        // If player is grounded => No gravity
        if (StateManager.IsGrounded)
        {
            velocity = 0;
        }
        // If player is falling
        else if (isFalling)
        {
            float newYVelocity = velocity + (gravity * fallMultiplier * Time.deltaTime);
            float nextYVelocity = Mathf.Max((velocity + newYVelocity) * 0.5f, -20.0f);
            velocity = nextYVelocity;
        }
        // If player is rising
        else
        {
            float newYVelocity = velocity + (gravity * Time.deltaTime);
            float nextYVelocity = (velocity + newYVelocity) * 0.5f;
            velocity = nextYVelocity;
        }
    }

    void HandleJump()
    {
        // Jump allowed if: Currently not in jump, On ground, Jump pressed
        if (!StateManager.InJump && StateManager.IsGrounded && inputManager.Jump > 0.0)
        {
            StateManager.InJump = true;
            velocity = initialJumpVelocity * 0.5f;
        }
        else if (!(inputManager.Jump > 0.0) && StateManager.IsGrounded)
        {
            StateManager.InJump = false;
        }
    }

    private void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2.0f;
        gravity = (-2.0f * maxJumpHeight) / Mathf.Pow(timeToApex, 2.0f);
        initialJumpVelocity = (2.0f * maxJumpHeight) / timeToApex;
    }
}