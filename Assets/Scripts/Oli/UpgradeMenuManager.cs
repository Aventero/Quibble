using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenuManager : MonoBehaviour
{
    public GameObject UpgradeMenu;
    public float showDelay = 2f;

    public bool IsVisible()
    {
        return UpgradeMenu.activeSelf;
    }

    public void StartShowUpgradeMenu()
    {
        StartCoroutine(ShowAfterDelay());
    }

    private void ShowUpgradeMenu()
    {
        UpgradeMenu.SetActive(true);
    }

    IEnumerator ShowAfterDelay()
    {
        yield return new WaitForSeconds(showDelay);

        ShowUpgradeMenu();
    }
}
