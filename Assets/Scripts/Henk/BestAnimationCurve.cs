using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestAnimationCurve : MonoBehaviour
{
    public AnimationCurve animationCurve;

    private void Start()
    {
        Debug.Log(animationCurve.Evaluate(0));
        Debug.Log(animationCurve.Evaluate(0.5f));
        Debug.Log(animationCurve.Evaluate(1f));
    }
}
