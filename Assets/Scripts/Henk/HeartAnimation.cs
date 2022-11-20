using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAnimation : MonoBehaviour
{
    public Animator HeartAnimator;

    private void Start()
    {
        Meteorite.OnPlanetHit += ShrinkHeartAnimaion;
    }

    private void ShrinkHeartAnimaion()
    {
        HeartAnimator.Play("HeartShrink");
    }

    private void OnDestroy()
    {
        Meteorite.OnPlanetHit -= ShrinkHeartAnimaion;
    }
}
