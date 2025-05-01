using TMPro;
using UnityEngine;

public class UIDropDown : UISelectable{
    private PlanetColoringManager planetColoringManager;
    private TMP_Dropdown dropdown;
    private bool isDropdownOpen = false;
    
    public void Start() {
        dropdown = GetComponent<TMP_Dropdown>();
        planetColoringManager = FindObjectOfType<PlanetColoringManager>();
    }

    public override void OnPointerEnter()
    {
        dropdown.image.color = planetColoringManager.GetCurrentColorUI();
    }

    public override void OnPointerExit() {
        dropdown.image.color = planetColoringManager.GetCurrentColorUI();
    }

    public override void OnPointerClick() {
        if (isDropdownOpen) {
            dropdown.Show();
            isDropdownOpen = false;
        }
        else {
            dropdown.Hide();
            isDropdownOpen = true;
        }
        
        Debug.Log("Dropdown OnPointerClick: " + isDropdownOpen);
    }

    public bool IsDropdownOpen() {
        return isDropdownOpen;
    }

    public override void OnPointerUp() { }
    public override void OnPointerDown() { }
}