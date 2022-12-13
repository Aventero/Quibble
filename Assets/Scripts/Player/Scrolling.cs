using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float addedDistance = 5.0f;
    private Camera zoomCamera;
    private float CenterDistance = 0;
    InputManager inputManager;
    private float scrollVal = 0;

    private void Start()
    {
        zoomCamera = Camera.main;
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CenterDistance = transform.position.magnitude;
        zoomCamera.orthographicSize = CenterDistance + addedDistance + scrollVal;


        scrollVal -= inputManager.Scrollvalue.y;
        if (scrollVal < -3f)
            scrollVal = -3f;
        if (scrollVal > 10f)
            scrollVal = 10f;
    }
}
