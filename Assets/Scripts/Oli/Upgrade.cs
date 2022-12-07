using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade/Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
    public enum UpgradeType
    {
        RANGE,
        ANGLE,
        HEAL,
        JUMP,
        MOVEMENT,
    }

    public UpgradeType Type;
    public Tier[] tiers;

    [System.Serializable]
    public class Tier
    {
        public Sprite upgradeSprite;
        public float upgradeEffect;
    }
}
