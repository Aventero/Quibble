using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    public static float SoundVolume = 0.5f;
    private static bool IsInitialized = false;

    public static void Initialize()
    {
        if (IsInitialized)
            return;

        if (!PlayerPrefs.HasKey("SoundVolume"))
        {
            SoundVolume = 0.5f;
            Save();
        }
        IsInitialized = true;
    }

    public static void Save()
    {
        PlayerPrefs.SetFloat("SoundVolume", SoundVolume);
        PlayerPrefs.Save();
    }

    public static void Load()
    {
        SoundVolume = PlayerPrefs.GetFloat("SoundVolume");
    }
}
