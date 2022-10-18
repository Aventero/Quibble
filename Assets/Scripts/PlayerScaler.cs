using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScaler : MonoBehaviour
{
    public float ScalingFactor = 0.1f;
    public Transform PivotPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            
    }

    private void FixedUpdate()
    {
        if (PivotPoint != null)
        {
            // Update is called once per frame
            float distance = Vector3.Distance(this.transform.position, PivotPoint.position);
            this.transform.localScale = Vector3.one * distance * ScalingFactor;
        }
    }
}
