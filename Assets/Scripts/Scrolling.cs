using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float ScrollSpeed = 0.1f;
    private Camera zoomCamera;
    public Transform PlayerTransform;
    public Transform PivotTransform;

    private void Start()
    {
        zoomCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        zoomCamera.orthographicSize = Vector2.Distance(PivotTransform.position, PlayerTransform.position) + 4;

        if (zoomCamera.orthographicSize < 1)
            zoomCamera.orthographicSize = 1;
    }
}
