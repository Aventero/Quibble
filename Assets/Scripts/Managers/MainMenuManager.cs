using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuManager : MonoBehaviour
{
    public ControllerAutoSelect ControllerAutoSelect;
    public PlayerInput PlayerInput;
    public GameLoader gameLoader;
    public GameObject Player;

    private void Start()
    {
        PlayerInput.SwitchCurrentActionMap("UI");
        UIManager.Instance.OpenedSubmenu(ControllerAutoSelect.AutoSelect);
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
