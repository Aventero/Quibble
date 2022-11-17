using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public GameObject DeathMenu;
    public TMPro.TMP_Text TMP_Text;

    private void Awake()
    {
        HealthManager.OnDeath += EnableDeathMenu;
        CameraShake.OnShakeEnd += StopGameTime;
        //DeathMenu = GameObject.FindGameObjectWithTag("DeathMenu");
    }

    private void EnableDeathMenu()
    {
        DeathMenu.SetActive(true);
        TMP_Text.SetText("You have reached stage:\n" + GameManager.Instance.CurrentStage);
    }

    private void OnDestroy()
    {
        HealthManager.OnDeath -= EnableDeathMenu;
        CameraShake.OnShakeEnd -= StopGameTime;
    }

    private void StopGameTime()
    {
        if (DeathMenu.activeSelf)
            Time.timeScale = 0f;
    }
}
