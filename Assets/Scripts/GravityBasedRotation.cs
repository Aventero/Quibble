using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gravity))]

public class GravityBasedRotation : MonoBehaviour
{
    private Gravity gravity;

    private void Awake()
    {
        gravity = GetComponent<Gravity>();
    }

    private void Update()
    {
        // Calculate angle from center to object
        float rotation = Mathf.Atan2(gravity.UpVector.y, gravity.UpVector.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(rotation, new Vector3(0, 0, 1));
    }
}
