using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;
    public static event UnityAction OnDeath;

    public bool IsDead { get; private set; }
    public Slider healthSlider;

    [Header("Health settings")]
    public float maxHealth = 100f;

    [Header("Animation")]
    public float lerpTime = 0.2f;
    
    // private float health = 10f;
    private float lastHealth = 100f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {
        lastHealth = PlayerStats.Instance.Health;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void UpdateValues()
    {
        // Update slider value
        StartCoroutine(UpdateProgress());
    }

    public void DealDamage(float amount)
    {
        // Deal damage;
        PlayerStats.Instance.Health -= amount;

        // Update slider value
        StartCoroutine(UpdateProgress());
    }

    IEnumerator UpdateProgress()
    {
        float deltaTime = 0.0f;
        while (deltaTime < lerpTime)
        {
            healthSlider.value = Mathf.Lerp(lastHealth, PlayerStats.Instance.Health, deltaTime / lerpTime);
            deltaTime += Time.deltaTime;
            yield return null;
        }

        healthSlider.value = PlayerStats.Instance.Health;
        lastHealth = PlayerStats.Instance.Health;

        // Check if player is dead
        if (PlayerStats.Instance.Health <= 0.0)
        {
            StateManager.IsDead = true;
            OnDeath.Invoke();
        }
    }
}
