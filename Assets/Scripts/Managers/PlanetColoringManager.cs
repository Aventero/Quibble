using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetColoringManager : MonoBehaviour
{
    public Material PlanetMaterial;
    public Material UIMaterial;

    void Update()
    {
        if (StateManager.IsDead)
        {
            PlanetMaterial.SetColor("_GlowColor", new Color(0, 0, 0, 0));
            return;
        }

        switch (SaveManager.Difficulty)
        {
            case Difficulty.Easy: 
                PlanetMaterial.SetColor("_GlowColor", new Color(0f, 7, 20, 0));
                UIMaterial.SetColor("_GlowColor", new Color(0f, 3, 10, 0));
                break;
            case Difficulty.Medium: 
                PlanetMaterial.SetColor("_GlowColor", new Color(20, 1, 15, 0)); 
                UIMaterial.SetColor("_GlowColor", new Color(10f, 0.5f, 6, 0));
                break;
            case Difficulty.Hard: 
                PlanetMaterial.SetColor("_GlowColor", new Color(10, 4, 0.15f, 0)); 
                UIMaterial.SetColor("_GlowColor", new Color(5f, 2, 0, 0));
                break;
            case Difficulty.Extreme: 
                PlanetMaterial.SetColor("_GlowColor", new Color(30, 0.1f, 0.5f, 0)); 
                UIMaterial.SetColor("_GlowColor", new Color(15f, 0.1f, 0.4f, 0));
                break;
        }
    }

    public Color GetCurrentColorUI()
    {
        switch (SaveManager.Difficulty)
        {
            case Difficulty.Easy: return new Color(0f, 0.5f, 0.9f, 1f);          // Cyan
            case Difficulty.Medium: return new Color(0.5f, 0.25f, 0.5f, 1f);    // Pink
            case Difficulty.Hard: return new Color(0.8f, 0.5f, 0.05f, 1f);      // yellow
            case Difficulty.Extreme: return new Color(0.6f, 0.15f, 0.15f, 1f);    // red 
            default: return new Color(0f, 0.5f, 0.9f, 1f);   // Cyan
        }
    }

}
