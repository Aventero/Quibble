using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowRotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, StateManager.AngleDeg - 90f);

        if (StateManager.IsDead)
        {
            enabled = false;
        }
    }
}
