using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade/Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
    public enum UpgradeType
    {
        ATTACK,
        HEALTH,
        JUMP,
        MOVEMENT,
        SLOW
    }

    public UpgradeType Type;
    public Tier[] tiers;

    [System.Serializable]
    public class Tier
    {
        public float spawnPercentage;
        public Sprite upgradeSprite;
        public float upgradeEffect;
    }
}
