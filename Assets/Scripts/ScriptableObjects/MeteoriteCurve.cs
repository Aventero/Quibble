using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Curve", menuName = "Meteorite/MeteoriteCurve", order = 1)]
public class MeteoriteCurve : ScriptableObject
{
    [Header("Curve Settings")]
    public GameObject Meteorite;
    public AnimationCurve SpawnsPerStage;
    public AnimationCurve TimeBetweenSpawns;

    public int GetSpawns(float stage)
    {
        return (int)SpawnsPerStage.Evaluate(stage);
    }

    public float GetTimeBetweenSpawns(float stage)
    {
        return TimeBetweenSpawns.Evaluate(stage);
    }
}