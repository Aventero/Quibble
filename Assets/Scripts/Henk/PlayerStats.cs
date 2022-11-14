using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float SwordLength = 1.0f;
    public float AttackAngle = 360.0f;
    public float AttackCooldown = 1.0f;
    public float PlayerSpeed = 1.0f;
    public float JumpHeight = 2.0f;
    Attack attack;

    void Start()
    {
        attack = GetComponent<Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        attack.UpgradeSwordLength(SwordLength);
        attack.UpgradeAttackAngle(AttackAngle);
        attack.UpgradeCooldown(AttackCooldown);
    }
}
