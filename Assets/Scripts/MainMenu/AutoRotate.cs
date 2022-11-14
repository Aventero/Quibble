using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float RotationSpeed = 1;

    private float currentRotation = 0;

    private void Update()
    {
        currentRotation += RotationSpeed * Time.deltaTime;

        if (currentRotation > 360)
            currentRotation = 0;

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentRotation));
    }
}
