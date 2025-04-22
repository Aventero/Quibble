using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuManager : MonoBehaviour
{
    public PlayerInput PlayerInput;
    public GameLoader gameLoader;
    public GameObject Player;
    public GameObject MainMenu;

    private void Start()
    {
        MainMenu.SetActive(false);
        PlayerInput.SwitchCurrentActionMap("UI");
    }

    public void StartGame()
    {
        PlayerInput.SwitchCurrentActionMap("Player");
        Player.GetComponent<PlayerInput>().currentActionMap.FindAction("Pause").Enable();
        gameLoader.LoadGameScene();
    }

    public void LoadTutorial()
    {
        PlayerInput.SwitchCurrentActionMap("Player");
        Player.GetComponent<PlayerInput>().currentActionMap.FindAction("Pause").Enable();
        gameLoader.LoadTutorialScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
