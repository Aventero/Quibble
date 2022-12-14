using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player")]
    public GameObject Player;

    [Header("Stage")]
    public int CurrentStage;
    public int MeteoritesHit { get; private set; }
    public int SpawnedMeteorites { get; private set; }
    public float StageMeteoriteCount { get; private set; }

    public List<MeteoriteCurve> meteorites;

    public AnimationCurve MeteoriteFallingCurveEasy;
    public AnimationCurve MeteoriteFallingCurveMedium;
    public AnimationCurve MeteoriteFallingCurveHard;
    public AnimationCurve MeteoriteFallingCurveExtreme;
    public TMP_Text StageText;
    
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
        StateManager.InMenu = false;
        StateManager.IsDead = false;
        StateManager.GamePaused = false;

        MeteoriteSpawner = GetComponent<MeteoriteSpawner>();
        StageProgressManager = GetComponent<StageProgressManager>();
        UpgradeMenuManager = GetComponent<UpgradeMenuManager>();
        Meteorite.OnMeteoriteHit += IncreaseMeteoriteHits;
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
        StartCoroutine(StartMeteorSpawners(1f));

        // Setup stage progress bar
        StageProgressManager.Setup();
    }

    public void StopSpawning()
    {
        MeteoriteSpawner.StopAllCoroutines();
    }

    private bool StageComplete()
    {
        if (MeteoriteSpawner.IsFinished() && MeteoritesHit >= StageMeteoriteCount)
            return true;
        return false;
    }

    public float GetFallingCurve(float v)
    {
        switch (SaveManager.Difficulty)
        {
            case Difficulty.Easy:
                return MeteoriteFallingCurveEasy.Evaluate(v);
            case Difficulty.Medium: 
                return MeteoriteFallingCurveMedium.Evaluate(v);
            case Difficulty.Hard: 
                return MeteoriteFallingCurveHard.Evaluate(v);
            case Difficulty.Extreme: 
                return MeteoriteFallingCurveExtreme.Evaluate(v);
        }

        // does not happen.
        Debug.Log("Difficulty curve not found! ");
        return MeteoriteFallingCurveEasy.Evaluate(v);
    }

    private void ResetMeteoriteCounter()
    {
        // Destroy every Meteorite that should not be there.
        GameObject[] meteorites = GameObject.FindGameObjectsWithTag("Meteorite");
        for (int i = 0; i < meteorites.Length; i++)
        {
            Destroy(meteorites[i].gameObject);
        }

        MeteoritesHit = 0;
        SpawnedMeteorites = 0;
        StageMeteoriteCount = 0;
    }

    IEnumerator ShowUpgradeWindow()
    {
        // Wait until stage bar is full
        while (StageProgressManager.IsFilling()) { yield return null; }

        UpgradeMenuManager.StartShowUpgradeMenu(0.5f);
    }

    IEnumerator StartMeteorSpawners(float timeInBetween)
    {
        foreach(MeteoriteCurve meteorCurve in meteorites)
        {
            int amount = meteorCurve.GetSpawns(CurrentStage);
            StageMeteoriteCount += amount;
        }

        // Reset how many meteorites are in this stage
        foreach (MeteoriteCurve meteorCurve in meteorites)
        {
            int amount = meteorCurve.GetSpawns(CurrentStage);
            MeteoriteSpawner.SpawnMeteoritesOverTime(meteorCurve.Meteorite, amount, meteorCurve.GetTimeBetweenSpawns(CurrentStage));
            yield return new WaitForSeconds(timeInBetween);
        }
    }
}
