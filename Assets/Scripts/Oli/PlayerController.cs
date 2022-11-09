using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputManager inputManager;
    
    public float radius = 1;
    public float movementSpeed = 1;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {
        // Calculate new angles
        StateManager.AngleRad -= inputManager.MovementInput.x * movementSpeed * Time.deltaTime;

        // Rotate player
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, StateManager.AngleDeg - 90f));

        // Polar -> Cartesian
        float xCoord = radius * Mathf.Cos(StateManager.AngleRad);
        float yCoord = radius * Mathf.Sin(StateManager.AngleRad);
        transform.position = new Vector2 (xCoord, yCoord);
    }
}