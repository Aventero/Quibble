using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    public static float SoundVolume;
    public static Difficulty Difficulty;
    public static int QualityLevel;
    private static bool IsInitialized = false;

    public static void Initialize()
    {
        if (IsInitialized)
        {
            Load();
            return;
        }

        if (!PlayerPrefs.HasKey("SoundVolume"))
        {
            SoundVolume = 0.5f;
            PlayerPrefs.SetFloat("SoundVolume", SoundVolume);
        }

        if (!PlayerPrefs.HasKey("Difficulty"))
        {
            Difficulty = Difficulty.Medium;
            PlayerPrefs.SetInt("Difficulty", (int)Difficulty);
        }

        if (!PlayerPrefs.HasKey("QualityLevel"))
        {
            QualityLevel = 5; // 0 - 5, veryLow - Ultra
            PlayerPrefs.SetInt("QualityLevel", QualityLevel);
        }

        Load();

        IsInitialized = true;
    }

    public static void Save()
    {
        PlayerPrefs.SetFloat("SoundVolume", SoundVolume);
        PlayerPrefs.SetInt("Difficulty", (int)Difficulty);
        PlayerPrefs.SetInt("QualityLevel", QualityLevel);
        PlayerPrefs.Save();
    }

    public static void Load()
    {
        SoundVolume = PlayerPrefs.GetFloat("SoundVolume");
        Difficulty = (Difficulty)PlayerPrefs.GetInt("Difficulty");
        QualityLevel = PlayerPrefs.GetInt("QualityLevel");
    }
}
