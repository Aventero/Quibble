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

    public TMPro.TMP_Text Congrats;
    public TMPro.TMP_Text Stage;

    public string[] CongratulationWords;

    [Header("Upgrades")]
    public Upgrade[] upgrades;
    public float[] tierPercentages;

    [Header("Slots")]
    public UpgradeSlot[] slots;

    public bool Visible => visible;

    private bool visible = false;

    public TMPro.TMP_Text RangeText;
    public TMPro.TMP_Text AngleText;
    public TMPro.TMP_Text HeightText;
    public TMPro.TMP_Text SpeedText;

    public void SetVisible(bool active)
    {
        visible = active;
    }

    // Start to show upgrade menu
    public void StartShowUpgradeMenu()
    {
        StartCoroutine(ShowAfterDelay());

        // Generate tier
        int tier = GenerateTier();

        // Generate upgrades
        foreach (UpgradeSlot slot in slots)
        {
            slot.GenerateUpgrade(upgrades, tier);
        }
    }

    public void Start()
    {
        RangeText.SetText(PlayerStats.Instance.Range + " M");
        AngleText.SetText(PlayerStats.Instance.Angle + " °");
        HeightText.SetText(PlayerStats.Instance.Jump + " M");
        SpeedText.SetText(PlayerStats.Instance.Movement + " km/s");
    }

    public void OnUpgradeChoosen(Upgrade.UpgradeType type, float effect)
    {
        switch (type)
        {
            case Upgrade.UpgradeType.RANGE:    
                PlayerStats.Instance.Range += effect;
                RangeText.SetText(PlayerStats.Instance.Range + " M");
                StartCoroutine(LerpColor(1f, 0f, 1f, RangeText));
                break;
            case Upgrade.UpgradeType.ANGLE:
                PlayerStats.Instance.Angle += effect;
                AngleText.SetText(PlayerStats.Instance.Angle + " °");
                StartCoroutine(LerpColor(1f, 0f, 1f, AngleText));
                break;
            case Upgrade.UpgradeType.HEAL:
                PlayerStats.Instance.Health += effect;
                if (PlayerStats.Instance.Health >= 100f) 
                    PlayerStats.Instance.Health = 100f; 
                HealthManager.Instance.UpdateValues();
                break;
            case Upgrade.UpgradeType.JUMP:
                PlayerStats.Instance.Jump += effect;
                HeightText.SetText(PlayerStats.Instance.Jump + " M");
                StartCoroutine(LerpColor(1f, 0f, 1f, HeightText));
                break;
            case Upgrade.UpgradeType.MOVEMENT:  
                PlayerStats.Instance.Movement += effect;
                SpeedText.SetText(PlayerStats.Instance.Movement + " km/s");
                StartCoroutine(LerpColor(1f, 0f, 1f, SpeedText));
                break;
        }

        // Hide upgrade menu
        UpgradeMenuVisibility(false);
        visible = false;

        // Start next stage
        GameManager.Instance.StartNextStage();
    }

    private void UpdateUpgradeText()
    {
        Congrats.SetText(CongratulationWords[Random.Range(0, CongratulationWords.Length)]);
        Stage.SetText("Stage " + GameManager.Instance.CurrentStage + " cleared!");
    }

    private void UpgradeMenuVisibility(bool visible)
    {
        UpgradeMenu.SetActive(visible);
    }

    IEnumerator ShowAfterDelay()
    {
        yield return new WaitForSeconds(showDelay);

        UpgradeMenuVisibility(true);
        UpdateUpgradeText();
    }

    IEnumerator LerpColor(float time, float minValue, float maxValue, TMPro.TMP_Text tMP_Text)
    {
        float elapsed = 0.0f;
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float intensity;
            if (elapsed / time <= 0.5f)
                intensity = Mathf.Lerp(minValue, maxValue, elapsed / time);
            else
            {
                intensity = Mathf.Lerp(maxValue, minValue, elapsed / time);
            }
            tMP_Text.color = Color.Lerp(new Color(1f, 1f, 0.5f), new Color(0, 1f, 0, 1f), intensity);
            tMP_Text.fontSize = Mathf.Lerp(23f, 30f, intensity);
            yield return null;
        }
        tMP_Text.color = new Color(1f, 1f, 1f, 1f);
        tMP_Text.fontSize = 23f;
        yield return null;
    }

    private int GenerateTier()
    {
        int tier = 0;

        // Random value between 0 - 100
        float random = Random.Range(0, 100f);

        float weight = 0;
        foreach (float tierChance in tierPercentages)
        {
            weight += tierChance;

            if (random < weight)
                break;

            tier++;
        }

        return tier;
    }
}
