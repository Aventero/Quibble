using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float ScrollSpeed = 1;
    private Camera zoomCamera;


    private void Start()
    {
        zoomCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        zoomCamera.orthographicSize -= Input.GetAxisRaw("Mouse ScrollWheel") * ScrollSpeed;
        
    }
}
