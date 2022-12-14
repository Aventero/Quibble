using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCurves : MonoBehaviour
{
    [SerializeField] private AnimationCurve WidthForJumpHeightRising;
    [SerializeField] private AnimationCurve WidthForJumpHeightFalling;
    [SerializeField] private AnimationCurve HeightForJumpHeightRising;
    [SerializeField] private AnimationCurve HeightForJumpHeightFalling;
    [SerializeField] private AnimationCurve BreathingCurveSquish;
    [SerializeField] private AnimationCurve BreathingCurveStretch;
    [SerializeField] private AnimationCurve LandingSquish;
    [SerializeField] private AnimationCurve LandingStretch;

    public float GetSpriteWidthRise(float jumpHeight)
    {
        return WidthForJumpHeightRising.Evaluate(jumpHeight);
    }

    public float GetSpriteWidthFall(float jumpHeight)
    {
        return WidthForJumpHeightFalling.Evaluate(jumpHeight);
    }

    public float GetSpriteHeightRising(float jumpHeight)
    {
        return HeightForJumpHeightRising.Evaluate(jumpHeight);
    }

    public float GetSpriteHeightFalling(float jumpHeight)
    {
        return HeightForJumpHeightFalling.Evaluate(jumpHeight);
    }

    public float GetBreathingSquish(float time)
    {
        return BreathingCurveSquish.Evaluate(time);
    }

    public float GetBreathingStretch(float time)
    {
        return BreathingCurveStretch.Evaluate(time);
    }

    public float GetLandingSquish(float time)
    {
        return LandingSquish.Evaluate(time);
    }

    public float GetLandingStretch(float time)
    {
        return LandingStretch.Evaluate(time);
    }
}
