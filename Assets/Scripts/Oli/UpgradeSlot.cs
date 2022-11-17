using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
    public Upgrade.UpgradeType upgradeType;
    public Upgrade.Tier tier;

    private Image upgradeImage;

    private void Start()
    {
        // Update button
        GetComponent<Button>().onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            FindObjectOfType<UpgradeMenuManager>().OnUpgradeChoosen(upgradeType, tier.upgradeEffect);
        }));
    }

    private void LoadReference()
    {
        if (upgradeImage == null)
            upgradeImage = GetComponent<Image>();
    }

    public void GenerateUpgrade(Upgrade[] upgrades)
    {
        LoadReference();

        // Get random upgrade type
        int type = Random.Range(0, upgrades.Length);
        Upgrade upgrade = upgrades[type];
        upgradeType = upgrade.Type;

        // Get random tier
        int randomTier = Random.Range(0, 100);

        int index = 0;
        float weight = 0;
        foreach (Upgrade.Tier tier in upgrade.tiers)
        {
            weight += tier.spawnPercentage;
            if (randomTier >= weight)
                index++;
            else
                break;
        }

        tier = upgrade.tiers[index];

        // Update texture
        upgradeImage.sprite = tier.upgradeSprite;
    }
}
