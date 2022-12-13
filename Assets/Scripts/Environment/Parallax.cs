using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Update is called once per frame
    public float rotationMultiplier = 0.8f;
    public float LerpSpeed = 1.0f;
    public Transform[] children;
    public InputManager InputManager;
    public Transform Player;
    public CameraMovement CameraMovement;

    private void Start()
    {
        children = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        for (int i = 0; i < children.Length; i++)
        {
            float l = Mathf.Lerp(0f, 1f, (float)i / (float)children.Length);
            children[i].rotation = Quaternion.Euler(new Vector3(0, 0, CameraMovement.currentAngle * l));
        }
    }
}
