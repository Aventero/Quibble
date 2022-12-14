using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager
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

    public static bool IsGrounded
    {
        get { return isGrounded; }
        set { isGrounded = value; }
    }

    public static bool InJump 
    { 
        get { return inJump; } 
        set { inJump = value; }
    }

    public static int MoveDirection { get; set; } = 1;

    private static float angleRad = 0;
    private static bool inJump = false;
    private static bool isGrounded = true;

    // GameState
    public static bool IsDead = false;
    public static bool InMenu = false;
    public static bool GamePaused = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void Init()
    {
        angleRad = 0;
        IsDead = false;
        inJump = false;
        isGrounded = false;
        InMenu = false;
        GamePaused = false;
    }
}
