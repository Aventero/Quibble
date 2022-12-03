using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stages : MonoBehaviour
{
    public MeteoriteStageData[] MeteoriteDataArray;
    public Stage[] GameStages;
    public bool CreateList = false;

    private void OnValidate()
    {
        if (CreateList)
        {
            CreateList = false;

            GameStages = new Stage[100];

            // Create the list
            for (int i = 0; i < MeteoriteDataArray.Length; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    GameStages[j] = new Stage(MeteoriteDataArray);
                }
            }
        }
    }
}
