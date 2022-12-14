using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UpgradeMenuManager : MonoBehaviour
{
    public GameObject UpgradeMenu;
    public TMPro.TMP_Text Congrats;
    public TMPro.TMP_Text Stage;

    public string[] CongratulationWords;

    [Header("Upgrades")]
    public Upgrade[] upgrades;
    public float[] tierPercentages;

    [Header("Slots")]
    public UpgradeSlot[] slots;

    public bool Visible => visible;

    public bool AutoStartNextStage = true;

    private bool visible = false;

    public TMPro.TMP_Text RangeText;
    public TMPro.TMP_Text AngleText;
    public TMPro.TMP_Text HeightText;
    public TMPro.TMP_Text SpeedText;

    private PlayerController player;

    public void SetVisible(bool active)
    {
        visible = active;
    }

    // Start to show upgrade menu
    public void StartShowUpgradeMenu(float delay = 2.0f)
    {
        StartCoroutine(ShowAfterDelay(delay));

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
        RangeText.SetText(System.Math.Round(PlayerStats.Instance.Range, 2) + " m");
        AngleText.SetText(System.Math.Round(PlayerStats.Instance.Angle, 2) + " °");
        HeightText.SetText(System.Math.Round(PlayerStats.Instance.Jump, 2) + " m");
        SpeedText.SetText(System.Math.Round(PlayerStats.Instance.Movement, 2) + " m/s");

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void OnUpgradeChoosen(Upgrade.UpgradeType type, float effect)
    {
        switch (type)
        {
            case Upgrade.UpgradeType.RANGE:    
                PlayerStats.Instance.Range += effect;
                RangeText.SetText(System.Math.Round(PlayerStats.Instance.Range, 2) + " m");
                StartCoroutine(LerpColor(1f, 0f, 1f, RangeText));
                break;
            case Upgrade.UpgradeType.ANGLE:
                PlayerStats.Instance.Angle += effect;
                AngleText.SetText(System.Math.Round(PlayerStats.Instance.Angle, 2) + " °");
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
                HeightText.SetText(System.Math.Round(PlayerStats.Instance.Jump, 2) + " m");
                StartCoroutine(LerpColor(1f, 0f, 1f, HeightText));
                break;
            case Upgrade.UpgradeType.MOVEMENT:  
                PlayerStats.Instance.Movement += effect;
                SpeedText.SetText(PlayerStats.Instance.Movement + " km/s");
                SpeedText.SetText(System.Math.Round(PlayerStats.Instance.Movement, 2) + " m/s");
                StartCoroutine(LerpColor(1f, 0f, 1f, SpeedText));
                break;
        }

        // Hide upgrade menu
        UpgradeMenuVisibility(false);
        player.PlayerControls.FindAction("Pause").Enable();
        visible = false;

        // Start next stage
        if (AutoStartNextStage)
            GameManager.Instance.StartNextStage();
    }

    private void UpdateUpgradeText()
    {
        Congrats.SetText(CongratulationWords[Random.Range(0, CongratulationWords.Length)]);
        Stage.SetText("Stage " + GameManager.Instance.CurrentStage + " cleared!");
    }

    public void UpgradeMenuVisibility(bool visible)
    {
        UpgradeMenu.SetActive(visible);
    }

    IEnumerator ShowAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        player.PlayerControls.FindAction("Pause").Disable();

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
