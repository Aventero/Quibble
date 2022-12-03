using UnityEngine;

[System.Serializable]
public class Stage
{
    public MeteoriteStageData[] MeteoriteAmounts;

    public Stage(MeteoriteStageData[] meteoriteStageDatass)
    {
        MeteoriteAmounts = meteoriteStageDatass;
    }
}
