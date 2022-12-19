using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Tween : MonoBehaviour
{
    RectTransform RectTransform;
    Image Image;
    Color SaveColor;
    PlanetColoringManager PlanetColoringManager;
    public Color DifficultyColor { get; set; }

    TMPro.TMP_Text TMP_Text;

    public UnityEvent UnityEvent;
    private bool isWobbling = false;


    private void Start()
    {
        RectTransform = GetComponent<RectTransform>();
        Image = GetComponent<Image>();
        PlanetColoringManager = FindObjectOfType<PlanetColoringManager>();
        TMP_Text = GetComponentInChildren<TMPro.TMP_Text>();
        TMP_Text.color = PlanetColoringManager.GetCurrentColorUI();
        DifficultyColor = PlanetColoringManager.GetCurrentColorUI();
        SaveColor = new Color(0.7f, 0.7f, 0.7f, 1.0f);
        Image.color = PlanetColoringManager.GetCurrentColorUI();
        TMP_Text.color = Color.black;
    }

    public void SetDifficultyColor()
    {
        DifficultyColor = PlanetColoringManager.GetCurrentColorUI();
        LeanTween.color(Image.rectTransform, DifficultyColor, 0.3f).setEaseOutBack().setIgnoreTimeScale(true);
    }

    public void OnMouseEnter()
    {
        LeanTween.scale(RectTransform, new Vector3(1.1f, 1.1f, 1.1f), 0.15f).setEaseOutBack().setIgnoreTimeScale(true);
        LeanTween.color(Image.rectTransform, SaveColor, 0.3f).setEaseOutBack().setIgnoreTimeScale(true);
    }

    public void OnMouseDown()
    {
        StartCoroutine(Wobble());
    }

    public void OnMouseUp()
    {
    }

    public void OnMouseClick()
    {
        StartCoroutine(WaitClickInvoke());
    }

    public void OnMouseExit()
    {
        StartCoroutine(WaitMouseExit());
    }

    IEnumerator Wobble()
    {
        isWobbling = true;
        LeanTween.scale(RectTransform, new Vector3(0.9f, 1.2f, 1.1f), 0.1f).setEaseOutBack().setIgnoreTimeScale(true) // Squish X
            .setOnComplete(end => LeanTween.scale(RectTransform, new Vector3(1.2f, 1.0f, 1.1f), 0.1f).setEaseOutBack().setIgnoreTimeScale(true) // Strech Y
            .setOnComplete(end => LeanTween.scale(RectTransform, new Vector3(1.1f, 1.1f, 1.1f), 0.05f).setEaseOutBack().setIgnoreTimeScale(true)
            .setOnComplete(() => isWobbling = false))).setIgnoreTimeScale(true); // BACK
        yield return null;
    }

    IEnumerator WaitMouseExit()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        LeanTween.scale(RectTransform, new Vector3(1.0f, 1.0f, 1.0f), 0.3f).setEaseOutBack().setIgnoreTimeScale(true);
        LeanTween.color(Image.rectTransform, DifficultyColor, 0.3f).setEaseOutBack().setIgnoreTimeScale(true);
        yield return null;
    }

    IEnumerator WaitClickInvoke()
    {
        yield return new WaitUntil(() => isWobbling == false);
        //RectTransform.localScale = new Vector3(1f, 1f, 1f);
        //Image.color = DifficultyColor;
        UnityEvent.Invoke();
        yield return new WaitForEndOfFrame();
    }

    void updateColorCallback(Color val)
    {
        TMP_Text.color = val;
    }

    private void OnDisable()
    {
        Image.color = DifficultyColor;
        RectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void OnDestroy()
    {
        Image.color = DifficultyColor;
        RectTransform.localScale = new Vector3(1f, 1f, 1f);
    }
}
