using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float minHeight = 5;

    [Header("Movement Settings")]
    public float maxJumpTime;
    public float fallMultiplier;

    [Header("Animation Settings")]
    private float scale;
    private JumpCurves jumpCurves;
    private float fallHeight;

    private InputManager inputManager;
    private float velocity;
    private float currentHeight;
    private float gravity;
    private float initialJumpVelocity;
    private float oldJumpHeight;

    private int moveDirection = 1;

    // Sound
    private bool shouldPlayLandingSound = false;


    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        jumpCurves = GetComponent<JumpCurves>();
        currentHeight = minHeight;
        scale = transform.localScale.x;
        oldJumpHeight = PlayerStats.Instance.Jump;

        // Setup jump
        SetupJumpVariables();
    }

    private void Update()
    {
        if (StateManager.IsDead)
            return;

        // Update jump variables
        if (oldJumpHeight != PlayerStats.Instance.Jump)
            SetupJumpVariables();

        // Movement & Rotation
        HandleMovement();

        // Apply velocity
        currentHeight += velocity * Time.deltaTime;

        CheckGrounded();
        PlaySounds();
        HandleGravity();
        HandleJump();
        SquishAndStretchPlayer();

        // Polar -> Cartesian
        float xCoord = currentHeight * Mathf.Cos(StateManager.AngleRad);
        float yCoord = currentHeight * Mathf.Sin(StateManager.AngleRad);
        transform.position = new Vector2 (xCoord, yCoord);
    }

    void SquishAndStretchPlayer()
    {
        // Store the height, the player is currently -> once he is falling -> fallHeight
        if (velocity > 0)
            fallHeight = currentHeight - minHeight + 0.00000000001f;

 
        if (velocity <= 0)
        {
            // Player is Falling
            float jumpHeight = currentHeight - minHeight + 0.00000000001f;
            float jumpHeightNormalized = Mathf.Lerp(1f, 0f, jumpHeight / fallHeight);
            Debug.Log(jumpHeightNormalized);
            transform.localScale = new Vector3(moveDirection * jumpCurves.GetSpriteWidthFall(jumpHeightNormalized), jumpCurves.GetSpriteHeightFalling(jumpHeightNormalized), transform.localScale.z);
        }
        else
        {
            // Player is Rising
            float velocityNormalized = Mathf.Lerp(1f, 0f, velocity / (initialJumpVelocity * 0.5f));
            transform.localScale = new Vector3(moveDirection * jumpCurves.GetSpriteWidthRise(velocityNormalized), jumpCurves.GetSpriteHeightRising(velocityNormalized), transform.localScale.z);
        }

        // Just to store the scale. just to be save
        if (StateManager.IsGrounded)
        {
            transform.localScale = new Vector3(moveDirection * scale, scale, transform.localScale.z);
        }
    }

    void HandleMovement()
    {
        // Moving Right, but was moving left
        if (inputManager.MovementInput.x >= 0 && transform.localScale.x < 0)
        {
            moveDirection = 1;
            transform.localScale = new Vector3(1 * transform.localScale.y, transform.localScale.y, transform.localScale.z);
        }

        // Moving Left, but was moving right
        if (inputManager.MovementInput.x < 0 && transform.localScale.x > 0)
        {
            moveDirection = -1;
            transform.localScale = new Vector3(-1 * transform.localScale.y, transform.localScale.y, transform.localScale.z);
        }


        // Calculate new angles
        StateManager.AngleRad -= inputManager.MovementInput.x * PlayerStats.Instance.Movement * Time.deltaTime;

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

    void PlaySounds()
    {
        if (StateManager.IsGrounded && shouldPlayLandingSound)
        {
            FindObjectOfType<AudioManager>().Play("Land");
            shouldPlayLandingSound = false;
        }

        // Player is on the ground, moving and the walking is not playing yet, then play the sound
        if (StateManager.IsGrounded && inputManager.MovementInput.x != 0 && !FindObjectOfType<AudioManager>().IsPLaying("Walk"))
        {
            FindObjectOfType<AudioManager>().Play("Walk");
        }

        if (StateManager.IsGrounded && inputManager.MovementInput.x == 0 || !StateManager.IsGrounded)
        {
            FindObjectOfType<AudioManager>().Stop("Walk");
        }
    }

    void HandleJump()
    {
        // Jump allowed if: Currently not in jump, On ground, Jump pressed
        if (!StateManager.InJump && StateManager.IsGrounded && inputManager.Jump > 0.0)
        {
            StateManager.InJump = true;
            velocity = initialJumpVelocity * 0.5f;
            FindObjectOfType<AudioManager>().Play("Jump");
            shouldPlayLandingSound = true;
        }
        else if (!(inputManager.Jump > 0.0) && StateManager.IsGrounded)
        {
            StateManager.InJump = false;
        }
    }

    private void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2.0f;
        gravity = (-2.0f * PlayerStats.Instance.Jump) / Mathf.Pow(timeToApex, 2.0f);
        initialJumpVelocity = (2.0f * PlayerStats.Instance.Jump) / timeToApex;
    }
}