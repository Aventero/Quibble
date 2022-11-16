using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Curve", menuName = "Meteorite/MeteoriteCurve", order = 1)]
public class MeteoriteCurve : ScriptableObject
{
    [Header("Bell Curve Settings")]
    public GameObject Meteorite;
    public float curveHeight = 0;
    public float curveWidth = 0;
    public float xAxisPosition = 0;

    [Header("Shows Start of the bellcurve")]
    public float CurveReachesOneAt = 0;

    private void OnValidate()
    {
        CurveReachesOneAt = StartingLevelForMeteoriteFunction(curveHeight, curveWidth, xAxisPosition);
    }

    private static float StartingLevelForMeteoriteFunction(float curveHeight, float curveWidth, float xAxisPosition)
    {
        float start = curveHeight * xAxisPosition - curveWidth * Mathf.Sqrt(curveHeight * Mathf.Log(curveHeight));
        start = start / curveHeight;
        return start;
    }
}