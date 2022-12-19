using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public RectTransform Logo;
    public GameObject MainMenu;
    public Image IntroBackground;

    // Start is called before the first frame update
    void Start()
    {
        if (StateManager.StartupDone)
        {
            // Reveal the menu on any other launch
            RevealMenu();
            return;
        }

        // Set to Mid
        Logo.localPosition = new Vector3(0, 60, 0);
        Logo.transform.localScale = new Vector3(3, 3, 3);

        IntroBackground.enabled = true;
        LeanTween.color(IntroBackground.rectTransform, new Color(0, 0, 0, 0), 4f).setOnComplete(() => IntroBackground.enabled = false);

        // Logo Thing only on first launch
        LeanTween.move(Logo, new Vector3(0, 60, 0), 2f) // Stay
            .setOnComplete(() => LeanTween.move(Logo, new Vector3(0, 360, 0), 2f).setEaseInOutBack());
        LeanTween.scale(Logo, new Vector3(3f, 3f, 3f), 2f) // Stay
            .setOnComplete(() => LeanTween.scale(Logo, new Vector3(1.8f, 1.8f, 1.8f), 2f).setEaseInOutBack()
            .setOnComplete(RevealMenu));

        // Reveal normally now
        StateManager.StartupDone = true;
    }

    private void RevealMenu()
    {
        MainMenu.SetActive(true);
    }
}
