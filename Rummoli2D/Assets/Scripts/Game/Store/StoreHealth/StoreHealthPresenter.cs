using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreHealthPresenter : IHealthStoreInfoProvider, IHealthStoreEventsProvider, IHealthStoreProvider
{
    private readonly StoreHealthModel _model;

    public StoreHealthPresenter(StoreHealthModel model)
    {
        _model = model;
    }

    public void Initialize()
    {
        _model.Initialize();
    }

    public void Dispose()
    {
        _model.Destroy();
    }

    #region Output

    public int MaxHealth => _model.MaxHealth;
    public int MaxShield => _model.MaxShield;

    public event Action<int, int> OnChangeMaxValues
    {
        add => _model.OnChangeMaxValues += value;
        remove => _model.OnChangeMaxValues -= value;
    }

    #endregion


    #region Input

    public void IncreaseMaxHealth(int amount) => _model.IncreaseMaxHealth(amount);

    public void IncreaseMaxShield(int amount) => _model.IncreaseMaxShield(amount);

    #endregion
}

public interface IHealthStoreInfoProvider
{
    int MaxHealth { get; }
    int MaxShield { get; }
}

public interface IHealthStoreEventsProvider
{
    event Action<int, int> OnChangeMaxValues;
}

public interface IHealthStoreProvider
{
    void IncreaseMaxHealth(int amount);
    void IncreaseMaxShield(int amount);
}
