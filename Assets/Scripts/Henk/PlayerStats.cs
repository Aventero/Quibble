using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public float Attack = 1.0f; // No meteorites with health
    public float Health = 10.0f;
    public float Jump = 3.0f;
    public float Movement = 2.0f;
    public float Slow = 1.0f;
}
