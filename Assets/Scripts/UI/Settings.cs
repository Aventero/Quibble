using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public TMPro.TMP_Dropdown QualityDropdown;

    private void Start()
    {
        SaveManager.Load();
        QualityDropdown.value = SaveManager.QualityLevel;
        SetQuality(SaveManager.QualityLevel);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        SaveManager.QualityLevel = qualityIndex;
        SaveManager.Save();
    }
}
