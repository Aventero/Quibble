using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float acceleration = 10f;
    public float drag = 0.95f;

    public Transform gravitationCenter;
    private Rigidbody2D rigid;

    private Vector2 downVector;
    private Vector2 downVectorNormalized;

    public Vector2 DownVector => downVector;
    public Vector2 UpVector => -downVector;
    public Vector2 DownVectorNormalized => downVector.normalized;
    public Vector2 ForwardVector => new Vector2(downVector.y, -downVector.x);

    public List<GameObject> pivotPoints;
    private Transform closestPivot;

    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        pivotPoints.AddRange(GameObject.FindGameObjectsWithTag("Planet"));

        closestPivot = pivotPoints[0].transform;
        foreach (GameObject pivot in pivotPoints)
        {
            float distanceToCurrentPivot = Vector2.Distance(transform.position, closestPivot.position);
            float distanceToPossiblePivot = Vector2.Distance(transform.position, pivot.transform.position);
            Debug.Log(distanceToPossiblePivot + " to " + pivot.name);
            if (distanceToPossiblePivot < distanceToCurrentPivot)
                closestPivot = pivot.transform;
        }

        Debug.Log("Closest Pivot = " + closestPivot.name);
    }

    public void Update()
    {
        FindClosestPivot();
    }

    public void FixedUpdate()
    {
        // Calculate direction
        this.transform.up = -(closestPivot.position - this.transform.position);
        downVector = (closestPivot.transform.position - gameObject.transform.position);
        downVectorNormalized = downVector.normalized;
        Debug.DrawRay(transform.position, downVectorNormalized);
        Debug.DrawRay(transform.position, rigid.velocity, Color.blue);
        rigid.velocity += downVectorNormalized * Time.fixedDeltaTime * acceleration;
        //rigid.velocity -= new Vector2(transform.right.x, transform.right.y) * drag;
        rigid.velocity *= drag;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        collision.gameObject.SetActive(false);
        pivotPoints.Remove(collision.gameObject);
        closestPivot = pivotPoints[0].transform;
        FindClosestPivot();
    }

    private void FindClosestPivot()
    {
        foreach (GameObject pivot in pivotPoints)
        {
            float distanceToCurrentPivot = Vector2.Distance(transform.position, closestPivot.position);
            float distanceToPossiblePivot = Vector2.Distance(transform.position, pivot.transform.position);
            if (distanceToPossiblePivot < distanceToCurrentPivot)
            {
                Debug.Log("New Pivot " + pivot.name);
                closestPivot = pivot.transform;
            }
        }
    }
}
