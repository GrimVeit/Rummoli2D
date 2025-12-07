using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthModel
{
    public int CurrentHealth { get; private set; }
    public int CurrentShield { get; private set; }

    public int MaxHealth { get; private set; }
    public int MaxShield { get; private set; }

    public event Action<int, int> OnHealthChanged;
    public event Action<int, int> OnShieldChanged;

    private readonly IHealthStoreInfoProvider _storeHealthInfoProvider;

    public PlayerHealthModel(IHealthStoreInfoProvider healthStoreInfoProvider)
    {
        _storeHealthInfoProvider = healthStoreInfoProvider;
    }

    public void Initialize()
    {
        MaxHealth = _storeHealthInfoProvider.MaxHealth;
        MaxShield = _storeHealthInfoProvider.MaxShield;

        CurrentHealth = MaxHealth;
        CurrentShield = MaxShield;

        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        OnShieldChanged?.Invoke(CurrentShield, MaxShield);

        Debug.Log("HEALTH: " + CurrentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (CurrentShield > 0)
        {
            int shieldDamage = Mathf.Min(damage, CurrentShield);
            CurrentShield -= shieldDamage;
            damage -= shieldDamage;
            OnShieldChanged?.Invoke(CurrentShield, MaxShield);
        }

        if (damage > 0)
        {
            CurrentHealth -= damage;
            if (CurrentHealth < 0) CurrentHealth = 0;
            OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        }

        Debug.Log("HEALTH: " + CurrentHealth);
    }

    public void AddHealth(int amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        Debug.Log("HEALTH: " + CurrentHealth);
    }

    public void AddShield(int amount)
    {
        CurrentShield += amount;
        if (CurrentShield > MaxShield) CurrentShield = MaxShield;
        OnShieldChanged?.Invoke(CurrentShield, MaxShield);
    }
}
