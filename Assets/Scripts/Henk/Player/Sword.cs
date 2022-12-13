using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sword : MonoBehaviour
{
    //public float StopDuration = 0.1f;
    private HitStop hitStop;
    public static event UnityAction OnMeteoriteHit;
    private ParticleManager particleManager;

    private void Start()
    {
        hitStop = FindObjectOfType<HitStop>();
        particleManager = FindObjectOfType<ParticleManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Meteorite"))
        {
            float duration = other.GetComponent<Meteorite>().ShakeDuration;
            hitStop.Stop(duration);
            OnMeteoriteHit.Invoke();

            // Spawn particles
            particleManager.SpawnEssence(this.transform.position);
        }
    }

    public static void ResetOnMeteoriteHit()
    {
        if (OnMeteoriteHit == null)
            return;

        foreach (System.Delegate invoker in OnMeteoriteHit.GetInvocationList())
            OnMeteoriteHit -= (UnityAction)invoker;
    }
}
