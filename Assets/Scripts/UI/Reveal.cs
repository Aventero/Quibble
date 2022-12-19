using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reveal : MonoBehaviour
{
    public GameObject[] MenuElements;
    public CanvasGroup CanvasGroup;

    private void OnEnable()
    {
        RevealMenu();
        CanvasGroup.blocksRaycasts = false;
    }

    private void RevealMenu()
    {
        for (int i = 0; i < MenuElements.Length; i++)
        {
            MenuElements[i].transform.localScale = new Vector3(0, 0, 0);
            if (i < MenuElements.Length - 1)
                LeanTween.scale(MenuElements[i], new Vector3(1f, 1f, 1f), (i + 1f) / 10f).setEaseOutBack().setIgnoreTimeScale(true);
            else
                LeanTween.scale(MenuElements[i], new Vector3(1f, 1f, 1f), (i + 1f) / 10f).setEaseOutBack().setOnComplete(() => CanvasGroup.blocksRaycasts = true).setIgnoreTimeScale(true);  // last

        }
    }
}
