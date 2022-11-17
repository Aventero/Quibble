using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeMenuManager : MonoBehaviour
{
    public GameObject UpgradeMenu;

    // Time to show menu
    public float showDelay = 2f;

    [Header("Upgrades")]
    public Upgrade[] upgrades;

    [Header("Slots")]
    public UpgradeSlot[] slots;

    public bool Visible => visible;

    private bool visible = false;

    public void SetVisible(bool active)
    {
        visible = active;
    }

    // Start to show upgrade menu
    public void StartShowUpgradeMenu()
    {
        StartCoroutine(ShowAfterDelay());

        // Generate upgrades
        foreach (UpgradeSlot slot in slots)
        {
            slot.GenerateUpgrade(upgrades);
        }
    }

    public void OnUpgradeChoosen(Upgrade.UpgradeType type, float percentage)
    {
        float multiplier = 1.0f + percentage / 100f;

        switch (type)
        {
            case Upgrade.UpgradeType.ATTACK:    
                PlayerStats.Instance.Attack *= multiplier;
                break;
            case Upgrade.UpgradeType.HEALTH:
                PlayerStats.Instance.Health += (HealthManager.Instance.maxHealth - PlayerStats.Instance.Health) * (percentage / 100f);
                HealthManager.Instance.UpdateValues();
                break;
            case Upgrade.UpgradeType.JUMP:
                PlayerStats.Instance.Jump *= multiplier;
                break;
            case Upgrade.UpgradeType.MOVEMENT:  
                PlayerStats.Instance.Movement *= multiplier;
                break;
            case Upgrade.UpgradeType.SLOW:      
                PlayerStats.Instance.Slow /= multiplier;
                break;
        }

        // Hide upgrade menu
        UpgradeMenuVisibility(false);
        visible = false;

        // Start next stage
        GameManager.Instance.StartNextStage();
    }

    private void UpgradeMenuVisibility(bool visible)
    {
        UpgradeMenu.SetActive(visible);
    }

    IEnumerator ShowAfterDelay()
    {
        yield return new WaitForSeconds(showDelay);

        UpgradeMenuVisibility(true);
    }
}
