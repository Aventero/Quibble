using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeteoriteSpawner : MonoBehaviour
{
    private bool isSpawning = false;
    private GameObject meteoritesEmpty;
    public static UnityAction OnMeteoriteSpawn;

    private void Awake()
    {
        meteoritesEmpty = new GameObject("Meteorites");
        meteoritesEmpty.transform.SetParent(transform);
    }

    public void SpawnMeteoritesOverTime(GameObject Meteor, int amount, float timeBetweenSpawning)
    {
        StartCoroutine(SpawnMeteorites(Meteor, amount, timeBetweenSpawning));
    }

    private IEnumerator SpawnMeteorites(GameObject Meteor, int amount, float timeBetweenSpawning)
    {
        isSpawning = true;
        while (amount > 0)
        {
            // Figure out position of meteorite
            float randomAngle = Random.Range(0, 360);
            float radius = 7.0f;
            Vector3 position = new Vector3(
                Mathf.Cos(randomAngle) * radius,
                Mathf.Sin(randomAngle) * radius,
                0.0f);

            // Spawn the meteor
            Instantiate(Meteor, position, new Quaternion(0, 0, 0, 0), meteoritesEmpty.transform);
            amount--;
            OnMeteoriteSpawn.Invoke();
            yield return new WaitForSeconds(timeBetweenSpawning);
        }
        isSpawning = false;
    }

    public bool IsFinished()
    {
        return !isSpawning;
    }
}
