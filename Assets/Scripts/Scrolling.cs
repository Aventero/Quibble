using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float ScrollSpeed = 0.1f;
    private Camera zoomCamera;

    private void Start()
    {
        zoomCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        zoomCamera.orthographicSize -= Input.mouseScrollDelta.y * ScrollSpeed;

        if (zoomCamera.orthographicSize < 1)
            zoomCamera.orthographicSize = 1;

        if (zoomCamera.orthographicSize > 10)
            zoomCamera.orthographicSize = 10;
    }
}
