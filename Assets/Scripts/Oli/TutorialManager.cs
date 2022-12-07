using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    /*
    Tutorial Stages:

    1. Welcome screen
    2. A/D Movement
    3. W/Space Movement
    4. Click/Enter Attack
    5. Meteorite hit on planet
    6. Meteorite smash
    7. Stage bar
    8. Upgrade menu
    9. Upgrade stats
    */

    public GameObject Player;
    public GameObject MeteoriteDmg;
    public GameObject MeteoriteNonDmg;

    public bool MovementUnlocked = false;
    public bool JumpUnlocked = false;
    public bool AttackUnlocked = false;

    private ExplanationManager dialogManager;
    private InputActionMap playerControls;
    private MeteoriteSpawner meteoriteSpawner;

    private int progress = 0;

    private void Start()
    {
        dialogManager = GetComponent<ExplanationManager>();
        meteoriteSpawner = GetComponent<MeteoriteSpawner>();

        // Get input controlls and deactivate everything
        playerControls = Player.GetComponent<PlayerInput>().currentActionMap;
        playerControls.Disable();

        StartCoroutine(StartTutorial());
    }

    public void UpdateProgress()
    {
        switch(progress)
        {
            case 0:
                playerControls.FindAction("Movement").Enable();
                StartCoroutine(dialogManager.StartNewExplanation(1, 5f));
                break;
            case 1:
                playerControls.FindAction("Jump").Enable();
                StartCoroutine(dialogManager.StartNewExplanation(2, 5f));
                break;
            case 2:
                playerControls.FindAction("Attack").Enable();
                StartCoroutine(dialogManager.StartNewExplanation(3, 5f));
                break;
            case 3:
                playerControls.Disable();
                meteoriteSpawner.SpawnMeteoritesOverTime(MeteoriteDmg, 1, 0.1f);
                StartCoroutine(dialogManager.StartNewExplanation(4, 10f));
                break;
            case 4:
                playerControls.Enable();
                meteoriteSpawner.SpawnMeteoritesOverTime(MeteoriteNonDmg, 1, 0.1f);
                TutorialMeteorite.OnPlanetHit += SpawnAnotherMeteorite;
                break;
        }

        progress++;
    }

    private void SpawnAnotherMeteorite()
    {
        dialogManager.StartExplanation(5);
        meteoriteSpawner.SpawnMeteoritesOverTime(MeteoriteNonDmg, 1, 0.1f);
    }

    IEnumerator StartTutorial()
    {
        yield return new WaitForSeconds(1f);

        dialogManager.StartExplanation(0);
    }
}
