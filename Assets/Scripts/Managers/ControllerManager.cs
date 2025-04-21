using UnityEngine;

public class ControllerManager : MonoBehaviour{
    private bool activatedController;
    
    private static ControllerManager _instance;
    public static ControllerManager Instance
    {
        get
        {
            return _instance;
        }
    }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject); // Destroy the entire GameObject, not just the component
        }
        else
        {
            _instance = this;
        }
    }

    public void ActivateGamepad()
    {
        activatedController = true;
        Cursor.visible = false;
    }

    public void DeactivateGamepad()
    {
        activatedController = false;
        Cursor.visible = true;
    }

    public bool ActiveController()
    {
        return activatedController;
    }
}