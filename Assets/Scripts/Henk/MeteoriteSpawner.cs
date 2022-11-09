using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour
{
    private IEnumerator coroutine;
    public GameObject Meteor;
    public float Spawnrate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        coroutine = SpawnMeteorite(Spawnrate);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator SpawnMeteorite(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            float randomAngle = Random.Range(0, 360);
            float radius = 7.0f;
            Vector3 position = new Vector3(
                Mathf.Cos(randomAngle) * radius,
                Mathf.Sin(randomAngle) * radius,
                0.0f);
            Instantiate(Meteor, position, new Quaternion(0, 0, 0, 0));
            Debug.Log("Spawning at " + position);
        }
    }
}
