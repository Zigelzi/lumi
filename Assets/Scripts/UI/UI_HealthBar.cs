using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    Health health;
    Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponentInParent<Health>();
        healthBar = GetComponent<Slider>();

        healthBar.maxValue = health.MaxHealth;
        healthBar.value = health.MaxHealth;

        health.ClientOnHealthUpdate += ClientUpdateHealthAmount;
    }

    void OnDestroy()
    {
        health.ClientOnHealthUpdate -= ClientUpdateHealthAmount;
    }

    void ClientUpdateHealthAmount(int newHealth, int maxHealth)
    {
        healthBar.value = newHealth;
    }
}
