using UnityEngine.UI;

public class UISlider : UISelectable{
    
    private PlanetColoringManager planetColoringManager;
    private Slider slider;
    
    public void Start()
    {
        slider = GetComponent<Slider>();
        planetColoringManager = FindObjectOfType<PlanetColoringManager>();
    }

    public void MoveSlider(int direction) {
        slider.value += direction * (slider.maxValue / 10f);
    }

    public override void OnPointerEnter()
    {
        slider.image.color = planetColoringManager.GetCurrentColorUI();
    }

    public override void OnPointerExit() {
        slider.image.color = slider.colors.normalColor;
    }

    public override void OnPointerClick() { }
    public override void OnPointerUp() { }
    public override void OnPointerDown() { }
}