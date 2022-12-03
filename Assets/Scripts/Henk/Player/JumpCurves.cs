using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCurves : MonoBehaviour
{
    [SerializeField] private AnimationCurve WidthForJumpHeightRising;
    [SerializeField] private AnimationCurve WidthForJumpHeightFalling;
    [SerializeField] private AnimationCurve HeightForJumpHeightRising;
    [SerializeField] private AnimationCurve HeightForJumpHeightFalling;
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
}
