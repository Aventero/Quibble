using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour
{
    public GameObject Meteor;
    public float TimeBetweenSpawning = 1.0f;
    public int MeteorsToSpawn = 10;
    private bool isSpawning = false;

    public void SpawnMeteoritesOverTime()
    {
        StartCoroutine(SpawnMeteorites());
    }

    public void AddMeteors(int amount)
    {
        MeteorsToSpawn += amount;
    }
    
    private IEnumerator SpawnMeteorites()
    {
        isSpawning = true;
        while (MeteorsToSpawn > 0)
        {
            // Figure out position of meteorite
            float randomAngle = Random.Range(0, 360);
            float radius = 7.0f;
            Vector3 position = new Vector3(
                Mathf.Cos(randomAngle) * radius,
                Mathf.Sin(randomAngle) * radius,
                0.0f);

            // Spawn the meteor
            Instantiate(Meteor, position, new Quaternion(0, 0, 0, 0));
            MeteorsToSpawn--;
            yield return new WaitForSeconds(TimeBetweenSpawning);
        }
        isSpawning = false;
    }
}
