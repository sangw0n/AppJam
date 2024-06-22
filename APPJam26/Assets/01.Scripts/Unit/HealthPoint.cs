using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    // CurrentHealth, MaxHealth
    public event Action<int, int> OnHealthChanged;
    public event Action OnDie;

    private int _currentHealth;
    private int _maxHealth;

    public void SetHealthInfo(int currentHealth, int maxHealth)
    {

        _currentHealth = currentHealth;
        _maxHealth = maxHealth;

        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);

    }

    public void AddValue(int value)
    {
      
        _currentHealth += value;
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);

        if (_currentHealth <= 0)
        {
            OnDie?.Invoke();
        }

    }

}
