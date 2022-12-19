using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotTween : MonoBehaviour
{
    RectTransform RectTransform;

    private void Start()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    public void OnEnter()
    {
        LeanTween.scale(RectTransform, new Vector3(1.1f, 1.1f, 1.1f), 0.1f).setEaseOutBack().setIgnoreTimeScale(true); // Squish X
        LeanTween.color(RectTransform, Color.white, 0.1f);
    }

    public void OnClick()
    {
        LeanTween.scale(RectTransform, new Vector3(1.2f, 1.2f, 1.1f), 0.05f).setEaseOutBack().setIgnoreTimeScale(true) // Squish X
            .setOnComplete(end => LeanTween.scale(RectTransform, new Vector3(0.8f, 0.8f, 1.1f), 0.05f).setEaseOutBack().setIgnoreTimeScale(true)); // Strech Y
    }

    public void OnExit()
    {
        LeanTween.scale(RectTransform, new Vector3(1.0f, 1.0f, 1.0f), 0.1f).setEaseOutBack().setIgnoreTimeScale(true);
        LeanTween.color(RectTransform, new Color(0.7f, 0.7f, 0.7f, 1f), 0.1f);
    }

    private void OnEnable()
    {
        if (RectTransform == null)
            return;

        LeanTween.color(RectTransform, new Color(0.7f, 0.7f, 0.7f, 1f), 0.1f);
    }
}
