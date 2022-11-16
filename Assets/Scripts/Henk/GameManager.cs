using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public TMP_Text MeteoriteText;
    public static GameManager Instance { get; private set; }
    public int CurrentStage { get; private set; }
    public int MeteoritesHit { get; private set; }
    public int SpawnedMeteorites { get; private set; }
    public float StageMeteoriteCount { get; private set; }

    public List<MeteoriteCurve> meteorites;

    private MeteoriteSpawner MeteoriteSpawner;
    private StageProgressManager StageProgressManager;

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
        Sword.OnMeteoriteHit += IncreaseMeteoriteHits;
        Meteorite.OnPlanetHit += IncreaseMeteoriteHits; // ##### has to change. DEBUG
        MeteoriteSpawner.OnMeteoriteSpawn += IncreaseCurrentMeteorites;
    }

    private void Update()
    {
        if (StageComplete())
        {
            StartNextStage();
        }
        MeteoriteText.SetText(
            "Stage: " + CurrentStage + 
            "\nHits:" + MeteoritesHit + 
            "\nLeft: " + (StageMeteoriteCount - MeteoritesHit) +
            "\nActive: " + (SpawnedMeteorites - MeteoritesHit));
    }

    private void IncreaseMeteoriteHits()
    {
        MeteoritesHit++;
    }

    private void IncreaseCurrentMeteorites()
    {
        SpawnedMeteorites++;
    }

    private void StartNextStage()
    {
        CurrentStage++;

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
        {
            ResetMeteoriteCounter();
            return true;
        }
        return false;
    }

    private void ResetMeteoriteCounter()
    {
        MeteoritesHit = 0;
        SpawnedMeteorites = 0;
        StageMeteoriteCount = 0;
    }
}
