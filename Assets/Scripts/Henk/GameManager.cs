using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public TMP_Text MeteoriteText;
    public GameManager Instance { get; private set; }
    public int CurrentStage { get; private set; }
    public int MeteoritesHit { get; private set; }
    public int SpawnedMeteorites { get; private set; }

    public float curveHeight = 10;
    public float curveWidth = 10;
    public float xAxisPosition = 10;
    public float start = 0;

    private MeteoriteSpawner MeteoriteSpawner;
    public List<MeteoriteCurve> meteorites;
    private int stageMeteoriteCount = 0;

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
            "\nLeft: " + (stageMeteoriteCount - MeteoritesHit) +
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
            MeteoriteSpawner.SpawnMeteoritesOverTime(meteor.Meteorite, amount);
            stageMeteoriteCount += amount;
        }
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
        if (MeteoriteSpawner.IsFinished() && MeteoritesHit >= stageMeteoriteCount)
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
        stageMeteoriteCount = 0;
    }
}
