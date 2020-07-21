using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth;

    public HealthBarScript HealthBar;

    bool onetime = false;
    [SerializeField] private GameObject healthBarCanvas;

    public event EventHandler<OnEnemyKilledEventArgs> OnEnemyKilled;
    public class OnEnemyKilledEventArgs : EventArgs
    {


    }

    public void Start()
    {
        CurrentHealth = MaxHealth;
        HealthBar.SetMaxHealth(MaxHealth);
    }

    void Update()
    {
        if (CurrentHealth <= 0)
        {
            if (!onetime)
            {
                Die();
                onetime = true;
            }
        }
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

    private void Die()
    {
        OnEnemyKilled?.Invoke(this, new OnEnemyKilledEventArgs()
        {

        });
        onetime = false;
        healthBarCanvas.SetActive(false);
    }
}
