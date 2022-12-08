using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Update is called once per frame
    private float rotation = 0;
    public float rotationMultiplier = 0.1f;
    public Transform[] children;
    public InputManager InputManager;
    public Transform Player;

    private void Start()
    {
        children = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        if (InputManager.MovementInput.x != 0)
        {
            rotation += InputManager.MovementInput.x * rotationMultiplier;
        }

        for (int i = 0; i < children.Length; i++)
        {
            float l = Mathf.Lerp(0f, 1f, (float)i / (float)children.Length);
            children[i].eulerAngles = Camera.main.transform.eulerAngles * l;
            //children[i].Rotate(new Vector3(0, 0, l * rotation), Space.World);
        }
    }
}
