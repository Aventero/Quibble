using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float StopDuration = 0.1f;
    private HitStop HitStop;

    private void Start()
    {
        HitStop = FindObjectOfType<HitStop>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Meteorite"))
        {
            HitStop.Stop(StopDuration);
        }
    }
}
