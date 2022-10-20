using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float ScrollSpeed = 0.1f;
    private Camera zoomCamera;
    public Transform PlayerTransform;
    public Transform PivotTransform;
    public static float CenterDistance = 0;

    private void Start()
    {
        zoomCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        CenterDistance = Vector2.Distance(PivotTransform.position, PlayerTransform.position);
        zoomCamera.orthographicSize = CenterDistance + 2;

        if (zoomCamera.orthographicSize < 1)
            zoomCamera.orthographicSize = 1;
    }
}