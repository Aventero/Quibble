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
    public float maxHealth = 10f;

    [Header("Animation")]
    public float lerpTime = 0.2f;
    
    private float health = 10f;
    private float lastHealth = 10f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {
        health = maxHealth;
        lastHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void DealDamage(float amount)
    {
        // Deal damage;
        health -= amount;

        // Update slider value
        StartCoroutine(UpdateProgress());
    }

    IEnumerator UpdateProgress()
    {
        float deltaTime = 0.0f;
        while (deltaTime < lerpTime)
        {
            healthSlider.value = Mathf.Lerp(lastHealth, health, deltaTime / lerpTime);
            deltaTime += Time.deltaTime;
            yield return null;
        }

        healthSlider.value = health;
        lastHealth = health;

        // Check if player is dead
        if (health <= 0.0)
        {
            OnDeath.Invoke();
        }
    }
}
