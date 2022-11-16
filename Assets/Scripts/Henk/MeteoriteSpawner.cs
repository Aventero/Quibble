using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeteoriteSpawner : MonoBehaviour
{
    public float TimeBetweenSpawning = 1.0f;
    private bool isSpawning = false;
    private GameObject meteoritesEmpty;
    public static UnityAction OnMeteoriteSpawn;

    private void Awake()
    {
        meteoritesEmpty = new GameObject("Meteorites");
        meteoritesEmpty.transform.SetParent(transform);
    }

    public void SpawnMeteoritesOverTime(GameObject Meteor, int amount)
    {
        StartCoroutine(SpawnMeteorites(Meteor, amount));
    }

    private IEnumerator SpawnMeteorites(GameObject Meteor, int amount)
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
            yield return new WaitForSeconds(TimeBetweenSpawning);
        }
        isSpawning = false;
    }

    public bool IsFinished()
    {
        return !isSpawning;
    }
}
