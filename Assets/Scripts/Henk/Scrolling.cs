using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float addedDistance = 5.0f;
    private Camera zoomCamera;
    public Transform PlayerTransform;
    private float CenterDistance = 0;

    private void Start()
    {
        zoomCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        CenterDistance = PlayerTransform.position.magnitude;
        zoomCamera.orthographicSize = CenterDistance + addedDistance;

        if (zoomCamera.orthographicSize < 1)
            zoomCamera.orthographicSize = 1;
    }
}
