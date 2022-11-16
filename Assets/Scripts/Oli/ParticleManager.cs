using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class ParticleManager : MonoBehaviour
{
    public GameObject[] particleSystems;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SpawnEssence(Vector3 spawnPosition)
    {
        // Instantiate new particle system
        GameObject particleSystem = Instantiate(particleSystems[0], transform);

        // Set spawn position
        particleSystem.GetComponent<VisualEffect>().SetVector3("Origin", spawnPosition);

        // Bind player position to target
        particleSystem.GetComponent<VFXCustomPropertyBinder>().Target = player;

        // Destroy particle system after animation played
        Destroy(particleSystem, particleSystem.GetComponent<VisualEffect>().GetFloat("Lifetime"));
    }
}
