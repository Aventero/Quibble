using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gravity))]

public class GravityBasedRotation : MonoBehaviour
{
    public float rotationSpeed = 1f;

    private Gravity gravity;

    private void Awake()
    {
        gravity = GetComponent<Gravity>();
    }

    private void Update()
    {
        // Get polar coordinate (angle)
        float rotation = (float)(Mathf.Atan(gravity.TopVector.y / gravity.TopVector.x) * 180.0 / Mathf.PI);

        // Correct calculated coordinate
        if (gravity.TopVector.x < 0)
            rotation += 180;

        if (gravity.TopVector.x > 0 && gravity.TopVector.y < 0)
            rotation += 360;

        float lerpRotation = Mathf.Lerp(transform.rotation.z, rotation, rotationSpeed);

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, lerpRotation));
    }
}
