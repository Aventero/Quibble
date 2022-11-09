using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float Acceleration = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Right
        if (Input.GetAxis("Horizontal") > 0)
        {
            rb2D.velocity = transform.right * Acceleration * Time.deltaTime;
        }

        // Left
        if (Input.GetAxis("Horizontal") < 0)
        {
            rb2D.velocity = -transform.right * Acceleration * Time.deltaTime;
        }
    }
}
