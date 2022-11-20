using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Update is called once per frame
    public float rotation = 0;
    public float rotationMultiplier = 0.1f;
    public Transform[] children;
    public InputManager InputManager;

    private void Start()
    {
        children = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        for (int i = 1; i < children.Length; i++)
        {
            children[i].transform.Rotate(Vector3.forward, InputManager.MovementInput.x * rotationMultiplier * i * Time.deltaTime);
        }
    }
}
