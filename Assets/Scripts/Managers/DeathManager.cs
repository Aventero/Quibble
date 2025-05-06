using UnityEngine;
using UnityEngine.InputSystem;

public class DeathManager : MonoBehaviour
{
    public GameObject DeathMenu;
    public GameObject AutoSelect; 
    public TMPro.TMP_Text TMP_Text;
    public PlayerInput playerInput;

    private void Awake()
    {
        HealthManager.OnDeath += EnableDeathMenu;
    }

    private void EnableDeathMenu()
    {
        GameManager.Instance.StopSpawning();
        FindObjectOfType<HeartWave>().StopBeating();

        // Show death screen
        DeathMenu.SetActive(true);
        TMP_Text.SetText("Du hast " + GameManager.Instance.CurrentStage + " Etappen geschafft!");
        
        playerInput.SwitchCurrentActionMap("UI");
        
        UIManager.Instance.OpenSubmenu(AutoSelect);

        // Disable gravity on meteors
        GameObject[] meteorites = GameObject.FindGameObjectsWithTag("Meteorite");
        for (int i = 0; i < meteorites.Length; i++)
        {
            meteorites[i].GetComponent<Meteorite>().Gravity = -1.0f;
        }
    }

    private void OnDestroy()
    {
        HealthManager.OnDeath -= EnableDeathMenu;
    }
}
