using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAnimation : MonoBehaviour
{
    public Animation Animation;

    private void Start()
    {
        Meteorite.OnPlanetHit += ShrinkHeartAnimaion;
    }

    private void ShrinkHeartAnimaion()
    {
        Animation.Play();
    }

    private void OnDestroy()
    {
        Meteorite.OnPlanetHit -= ShrinkHeartAnimaion;
    }
}
