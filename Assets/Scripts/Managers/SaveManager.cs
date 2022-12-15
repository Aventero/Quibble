using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    public static float SoundVolume;
    public static Difficulty Difficulty;
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
        Load();

        IsInitialized = true;
    }

    public static void Save()
    {
        PlayerPrefs.SetFloat("SoundVolume", SoundVolume);
        PlayerPrefs.SetInt("Difficulty", (int)Difficulty);
        PlayerPrefs.Save();
    }

    public static void Load()
    {
        SoundVolume = PlayerPrefs.GetFloat("SoundVolume");
        Difficulty = (Difficulty)PlayerPrefs.GetInt("Difficulty");
    }
}
