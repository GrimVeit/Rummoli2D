using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreHealthModel
{
    private readonly string MAX_HEALTH_KEY = "MAX_HEALTH";
    private readonly string MAX_SHIELD_KEY = "MAX_SHIELD";

    public int MaxHealth { get; private set; }
    public int MaxShield { get; private set; }

    public event Action<int, int> OnChangeMaxValues;

    public StoreHealthModel(string healthKey, string shieldKey)
    {
        MAX_HEALTH_KEY = healthKey;
        MAX_SHIELD_KEY = shieldKey;
    }

    public void Initialize()
    {
        MaxHealth = PlayerPrefs.GetInt(MAX_HEALTH_KEY, 2);
        MaxShield = PlayerPrefs.GetInt(MAX_SHIELD_KEY, 2);

        OnChangeMaxValues?.Invoke(MaxHealth, MaxShield);
    }

    public void Destroy()
    {
        PlayerPrefs.SetInt(MAX_HEALTH_KEY, MaxHealth);
        PlayerPrefs.SetInt(MAX_SHIELD_KEY, MaxShield);
    }

    public void IncreaseMaxHealth(int amount)
    {
        MaxHealth += amount;
        OnChangeMaxValues?.Invoke(MaxHealth, MaxShield);
    }

    public void IncreaseMaxShield(int amount)
    {
        MaxShield += amount;
        OnChangeMaxValues?.Invoke(MaxHealth, MaxShield);
    }
}
