using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [Header("References")]
    public GameObject Player;
    public GameObject MeteoriteDamage;
    public GameObject MeteoriteNoDamage;
    public GameObject StageBar;
    public GameObject StatsLeft;
    public GameObject StatsRight;
    public GameObject[] UpgradeSlots;

    private ExplanationManager explanationManager;
    private InputActionMap playerControls;
    private GameLoader gameLoader;
    private UpgradeMenuManager upgradeMenuManager;
    private Slider stageSlider;

    private void Start()
    {
        // Get necessary references
        explanationManager = GetComponent<ExplanationManager>();
        stageSlider = StageBar.GetComponentInChildren<Slider>();
        upgradeMenuManager = GetComponent<UpgradeMenuManager>();
        gameLoader = FindObjectOfType<GameLoader>();

        upgradeMenuManager.AutoStartNextStage = false;

        // Get input controlls and deactivate everything
        playerControls = Player.GetComponent<PlayerInput>().currentActionMap;
        playerControls.Disable();

        // Start tutorial
        StartCoroutine(StartTutorial());
    }

    IEnumerator StartTutorial()
    {
        // Wait for fade to finish
        yield return new WaitForSeconds(1f);

        // Start first explanation (move)
        ExplanationManager.OnExplanationTrigger += UnlockedMovement;
        ExplanationManager.OnExplanationSecondaryTrigger += () => playerControls.FindAction("Movement").Enable();
        explanationManager.StartExplanation(0);
    }

    private void UnlockedMovement()
    {
        ExplanationManager.ResetTrigger(true);

        // Start second explanation (jump)
        ExplanationManager.OnExplanationSecondaryTrigger += () => playerControls.FindAction("Jump").Enable();
        ExplanationManager.OnExplanationTrigger += UnlockedJump;
        StartCoroutine(explanationManager.StartNewExplanation(1, 5f));
    }

    private void UnlockedJump()
    {
        ExplanationManager.ResetTrigger(true);

        // Start third explanation (attack)
        ExplanationManager.OnExplanationSecondaryTrigger += () => playerControls.FindAction("Attack").Enable();
        ExplanationManager.OnExplanationTrigger += UnlockedAttack;
        StartCoroutine(explanationManager.StartNewExplanation(2, 5f));
    }

    private void UnlockedAttack()
    {
        ExplanationManager.ResetTrigger(true);

        // Start fourth explanation (approaching meteorite)
        ExplanationManager.OnExplanationTrigger += () =>
        {
            // Disable input
            playerControls.Disable();

            // Spawn meteorite
            Instantiate(MeteoriteDamage, new Vector3(0.0f, 4.0f, 0.0f), new Quaternion(0, 0, 0, 0));
        };
        Meteorite.OnPlanetHit += MeteoriteDamageTaken;
        StartCoroutine(explanationManager.StartNewExplanation(3, 5f));
    }

    private void MeteoriteDamageTaken()
    {
        ExplanationManager.ResetTrigger(false);
        Meteorite.OnPlanetHit -= MeteoriteDamageTaken;

        // Start fith explanation (damage)
        ExplanationManager.OnExplanationTrigger += MeteoriteHitTraining;
        StartCoroutine(explanationManager.StartNewExplanation(4, 0.0f));
    }

    private void MeteoriteHitTraining()
    {
        ExplanationManager.ResetTrigger(false);

        // Start meteorite hit training
        playerControls.Enable();
        Instantiate(MeteoriteNoDamage, new Vector3(0.0f, 7.0f, 0.0f), new Quaternion(0, 0, 0, 0));

        // If meteorite hits planet => Spawn new one
        Meteorite.OnPlanetHit += MeteoriteHitFailure;

        // If meteorite is slabbed => Continue
        Sword.OnMeteoriteHit += MeteoriteHitSuccess;
    }

    private void MeteoriteHitFailure()
    {
        explanationManager.StartExplanation(5);
        Instantiate(MeteoriteNoDamage, new Vector3(0.0f, 7.0f, 0.0f), new Quaternion(0, 0, 0, 0));
    }

    private void MeteoriteHitSuccess()
    {
        Sword.OnMeteoriteHit -= MeteoriteHitSuccess;

        // Start explanation (stages)
        ExplanationManager.OnExplanationTrigger += StageProgress;
        ExplanationManager.OnExplanationSecondaryTrigger += () =>
        {
            // Setup & show stage
            StageBar.SetActive(true);
            stageSlider.maxValue = 1;
            stageSlider.value = 0;
        };
        explanationManager.StartExplanation(6);
    }

    private void StageProgress()
    {
        ExplanationManager.ResetTrigger(true);

        Sword.OnMeteoriteHit += MeteoriteHitSuccessStageUpdate;
        Instantiate(MeteoriteNoDamage, new Vector3(0.0f, 7.0f, 0.0f), new Quaternion(0, 0, 0, 0));
    }

    private void MeteoriteHitSuccessStageUpdate()
    {
        StartCoroutine(StageFillUp());
    }

    IEnumerator StageFillUp()
    {
        float deltaTime = 0.0f;
        while (deltaTime < 0.2)
        {
            stageSlider.value = Mathf.Lerp(0.0f, 1.0f, deltaTime / 0.2f);
            deltaTime += Time.deltaTime;
            yield return null;
        }

        stageSlider.value = 1;

        // Show upgrade menu
        yield return new WaitForSeconds(2f);
        UpgradeMenu();
    }

    private void UpgradeMenu()
    {
        ExplanationManager.OnExplanationTrigger += UpgradeStats;
        upgradeMenuManager.StartShowUpgradeMenu(0.0f);
        explanationManager.StartExplanation(7);
    }

    private void UpgradeStats()
    {
        ExplanationManager.ResetTrigger();

        // Start explanation (stats)
        ExplanationManager.OnExplanationSecondaryTrigger += () =>
        {
            StatsLeft.SetActive(true);
            StatsRight.SetActive(true);
        };
        ExplanationManager.OnExplanationTrigger += EnableUpgrades;
        StartCoroutine(explanationManager.StartNewExplanation(8, 5f));
    }

    private void EnableUpgrades()
    {
        ExplanationManager.ResetTrigger(true);

        // Activate upgrade slots
        foreach (GameObject slot in UpgradeSlots)
        {
            slot.GetComponent<TutorialUpgradeSlot>().AddListener();
            slot.GetComponent<Button>().onClick.AddListener(UpgradeSelected);
        }
    }

    private void UpgradeSelected()
    {
        // Start final explanation
        ExplanationManager.OnExplanationTrigger += gameLoader.LoadMainMenuScene;
        StartCoroutine(explanationManager.StartNewExplanation(9, 1f));
    }

    private void OnDestroy()
    {
        Debug.Log("GsONE");
    }
}
