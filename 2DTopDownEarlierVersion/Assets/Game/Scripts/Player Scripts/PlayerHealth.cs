using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth;

    public HealthBarScript HealthBar;

    public void Start()
    {
        CurrentHealth = MaxHealth;
        HealthBar.SetMaxHealth(MaxHealth);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        HealthBar.SetHealth(CurrentHealth);
    }
    public void Heal(int heal)
    {
        CurrentHealth += heal;

        HealthBar.SetHealth(CurrentHealth);
    }

    private void Update()
    {
      //  Debug.Log(CurrentHealth);
    }
}
