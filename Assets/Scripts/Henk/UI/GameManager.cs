using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public TMP_Text StageText;
    public static GameManager Instance { get; private set; }
    public int CurrentStage { get; private set; }
    public int MeteoritesHit { get; private set; }
    public int SpawnedMeteorites { get; private set; }
    public float StageMeteoriteCount { get; private set; }

    public List<MeteoriteCurve> meteorites;

    private MeteoriteSpawner MeteoriteSpawner;
    private StageProgressManager StageProgressManager;
    private UpgradeMenuManager UpgradeMenuManager;

    private void Awake()
    {
        // Make sure there is only this instance of the GameManager
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {
        MeteoriteSpawner = GetComponent<MeteoriteSpawner>();
        StageProgressManager = GetComponent<StageProgressManager>();
        UpgradeMenuManager = GetComponent<UpgradeMenuManager>();
        Sword.OnMeteoriteHit += IncreaseMeteoriteHits;
        Meteorite.OnPlanetHit += IncreaseMeteoriteHits;
        MeteoriteSpawner.OnMeteoriteSpawn += IncreaseCurrentMeteorites;

        // Start first stage
        StartNextStage();
    }

    private void Update()
    {
        if (StageComplete() && !UpgradeMenuManager.Visible)
        {
            // Show Upgrade menu
            UpgradeMenuManager.SetVisible(true);
            StartCoroutine(ShowUpgradeWindow());
        }
        StageText.SetText("Stage: " + CurrentStage);
    }

    private void IncreaseMeteoriteHits()
    {
        MeteoritesHit++;
    }

    private void IncreaseCurrentMeteorites()
    {
        SpawnedMeteorites++;
    }

    public void StartNextStage()
    {
        CurrentStage++;

        ResetMeteoriteCounter();

        // Reset how many meteorites are in this stage
        foreach (MeteoriteCurve meteor in meteorites)
        {
            int amount = MeteoriteFunction(CurrentStage, meteor.curveHeight, meteor.curveWidth, meteor.xAxisPosition);
            MeteoriteSpawner.SpawnMeteoritesOverTime(meteor.Meteorite, amount, meteor.timeBetweenSpawns);
            StageMeteoriteCount += amount;
        }

        // Setup stage progress bar
        StageProgressManager.Setup();
    }

    private int MeteoriteFunction(int stage, float curveHeight, float curveWidth, float xAxisPosition)
    {
        // floor(k e^(-k ((x - b) / n)²))
        float exponent = -curveHeight * Mathf.Pow((stage - xAxisPosition) / curveWidth, 2);
        int meteorites = (int)(curveHeight * Mathf.Exp(exponent));
        return meteorites;
    }

    private bool StageComplete()
    {
        if (MeteoriteSpawner.IsFinished() && MeteoritesHit >= StageMeteoriteCount)
            return true;
        return false;
    }

    private void ResetMeteoriteCounter()
    {
        MeteoritesHit = 0;
        SpawnedMeteorites = 0;
        StageMeteoriteCount = 0;
    }

    IEnumerator ShowUpgradeWindow()
    {
        // Wait until stage bar is full
        while (StageProgressManager.IsFilling()) { yield return null; }

        UpgradeMenuManager.StartShowUpgradeMenu();
    }
}
