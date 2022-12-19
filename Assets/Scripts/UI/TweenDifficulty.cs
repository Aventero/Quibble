using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TweenDifficulty : MonoBehaviour
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
        DifficultyColor = Image.color;
        SaveColor = new Color(0.7f, 0.7f, 0.7f, 1.0f);
        TMP_Text.color = Color.black;
    }

    public void OnMouseEnter()
    {
        LeanTween.scale(RectTransform, new Vector3(1.1f, 1.1f, 1.1f), 0.3f).setEaseOutBack();
        LeanTween.color(Image.rectTransform, SaveColor, 0.3f).setEaseOutBack();
        //LeanTween.value(gameObject, updateColorCallback, Color.white, DifficultyColor, 0.3f);
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
        LeanTween.scale(RectTransform, new Vector3(1.2f, 1.1f, 1.1f), 0.1f).setEaseOutBack() // Squish X
            .setOnComplete(end => LeanTween.scale(RectTransform, new Vector3(0.9f, 1.3f, 1.1f), 0.1f).setEaseOutBack() // Strech Y
            .setOnComplete(end => LeanTween.scale(RectTransform, new Vector3(1.1f, 1.1f, 1.1f), 0.1f).setEaseOutBack()
            .setOnComplete(() => isWobbling = false))); // BACK
        yield return new WaitForEndOfFrame();
    }

    IEnumerator WaitMouseExit()
    {
        yield return new WaitForSeconds(0.1f);
        LeanTween.scale(RectTransform, new Vector3(1.0f, 1.0f, 1.0f), 0.3f).setEaseOutBack();
        LeanTween.color(Image.rectTransform, DifficultyColor, 0.3f).setEaseOutBack();
        yield return new WaitForEndOfFrame();
    }

    IEnumerator WaitClickInvoke()
    {
        yield return new WaitUntil(() => isWobbling == false);
        RectTransform.localScale = new Vector3(1f, 1f, 1f);
        Image.color = DifficultyColor;
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
