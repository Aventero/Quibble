using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gravity))]
public class GravityBasedRotation : MonoBehaviour
{
    private Gravity gravity;

    private void Awake()
    {
        gravity = GetComponent<Gravity>();
    }

    private void Update()
    {
        
    }
}
