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
        AddListener();
    }

    public void AddListener()
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

    public void SetUpgrade(Upgrade upgrade, int tierLevel)
    {
        LoadReference();

        // Select type
        upgradeType = upgrade.Type;
        
        // Select correct tier
        tier = upgrade.tiers[tierLevel];
        
        // Update texture
        upgradeImage.sprite = tier.upgradeSprite;
    }
}
