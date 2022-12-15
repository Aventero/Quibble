using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public void SetDifficulty(int difficulty)
    {
        SaveManager.Difficulty = (Difficulty)difficulty;
        SaveManager.Save();
    }
}
