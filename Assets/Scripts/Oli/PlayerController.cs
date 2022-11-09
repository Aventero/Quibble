using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 MovementInput;

    public float radius;
    public float angle = Mathf.PI / 2f;

    public float speed;

    private void Update()
    {
        // Modify coordinates
        angle -= MovementInput.x * speed * Time.deltaTime;
        
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * angle - 90));

        // Polar -> Cartesian
        float xCoord = radius * Mathf.Cos(angle);
        float yCoord = radius * Mathf.Sin(angle);
        transform.position = new Vector2 (xCoord, yCoord);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }
}