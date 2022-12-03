using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MeteoriteStageData
{
    [Header("Data")]
    public GameObject Meteorite;
    public int Amount = 10;
    public bool IsRandom = false;
}
