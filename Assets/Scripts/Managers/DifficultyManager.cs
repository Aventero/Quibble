using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public void SetDifficulty(int difficulty)
    {
        SaveManager.Difficulty = (Difficulty)difficulty;
        SaveManager.Save();
        Tween[] tweens = FindObjectsOfType<Tween>();
        for (int i = 0; i < tweens.Length; i++)
        {
            tweens[i].SetDifficultyColor();
        }
    }
}
