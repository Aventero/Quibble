using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControllerAutoSelect : MonoBehaviour
{
    public Button AutoSelect; 

    void Start()
    {
        if (Gamepad.all.Count > 0)
            AutoSelect.Select();
    }

    public void Select()
    {
        if (Gamepad.all.Count > 0)
            AutoSelect.Select();
    }
}
