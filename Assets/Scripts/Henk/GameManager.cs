using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Bois to clap funciton
    // How many have been clapped
    // How many are remaining
    // Current stage
    // Current time
    // Progress

    public static GameManager Instance { get; private set; }
    private int stageMeteorites = 10;
    private int remainingMeteoritesToSpawn;
    private int meteoritesInAir;
    private int meteoriteHitCount = 0;
    private int currentStage = 1;
    private int progress = 0; // Basically how many meteorites have been hit out of the stage max

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
}
