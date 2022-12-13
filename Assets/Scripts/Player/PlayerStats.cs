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

    public float Range = 1f; 
    public float Angle = 200f;
    public float Health = 100f;
    public float Jump = 3f;
    public float Movement = 2f;
}
