using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : ScriptableObject
{
    // Player
    public static float AngleRad
    {
        get { return angleRad; }
        set
        {
            angleRad = (AngleRad < 0) ? 2 * Mathf.PI : value;
            angleRad = (AngleRad > 2 * Mathf.PI) ? 0 : value;
        }
    }

    public static float AngleDeg
    { 
        get { return AngleRad * Mathf.Rad2Deg; }
    }

    private static float angleRad = 0;
}
