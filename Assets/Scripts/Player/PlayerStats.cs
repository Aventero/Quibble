using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public AnimationCurve RangeCurve;
    public AnimationCurve AngleCurve;
    public AnimationCurve JumpCurve;
    public AnimationCurve MovementCurve;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    // Level
    public int RangeLevel = 1;
    public int AngleLevel = 1;
    public int JumpLevel = 1;
    public int MovementLevel = 1;

    // Actual Values
    public float Range { get { return RangeCurve.Evaluate(RangeLevel); } }
    public float Angle { get { return AngleCurve.Evaluate(AngleLevel); } }
    public float Jump { get { return JumpCurve.Evaluate(JumpLevel); } }
    public float Movement { get { return MovementCurve.Evaluate(MovementLevel); } }

    public float Health = 100f;
}
