using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float followSpeed = 0.1f;

    public float currentAngle = 0.0f;

    private void Update()
    {
        // Lerp current angle to player angle
        currentAngle = Mathf.LerpAngle(currentAngle, StateManager.AngleDeg, followSpeed * Time.deltaTime);

        // Set rotation (-90 deg correction)
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle - 90));
    }
}
