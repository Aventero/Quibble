using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteAlways]
public class Scaler : MonoBehaviour
{
    public Transform PivotPoint;
    public float ScalingFactor = 0.1f;
    public Vector3 StandardScale = Vector3.one;


    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {

    }

    private void FixedUpdate()
    {
        // Update is called once per frame
        //float distance = Vector3.Distance(this.transform.position, PivotPoint.position);
        //this.transform.localScale = StandardScale * distance * ScalingFactor;

        // Lookat in 2D 
        //this.transform.up = -(PivotPoint.position - this.transform.position);
    }

    private void OnGUI()
    {
        if (!Application.isPlaying)
        {
            // Update is called once per frame
            //float distance = Vector3.Distance(this.transform.position, PivotPoint.position);
            //this.transform.localScale = StandardScale * distance * ScalingFactor;

            // Lookat in 2D 
            //this.transform.up = -(PivotPoint.position - this.transform.position);
        }
    }
}
