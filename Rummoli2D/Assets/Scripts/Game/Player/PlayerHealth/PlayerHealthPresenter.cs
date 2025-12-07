using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthPresenter : IPlayerHealthInfoProvider, IPlayerHealthEventsProvider, IPlayerHealthProvider
{
    private readonly PlayerHealthModel _model;
    private readonly PlayerHealthView _view;

    public PlayerHealthPresenter(PlayerHealthModel model, PlayerHealthView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        _model.OnHealthChanged += _view.UpdateHealth;
        _model.OnShieldChanged += _view.UpdateShield;
    }

    private void DeactivateEvents()
    {
        _model.OnHealthChanged -= _view.UpdateHealth;
        _model.OnShieldChanged -= _view.UpdateShield;
    }

    public int CurrentHealth => _model.CurrentHealth;
    public int CurrentShield => _model.CurrentShield;

    public event Action<int, int> OnHealthChanged
    {
        add => _model.OnHealthChanged += value;
        remove => _model.OnHealthChanged -= value;
    }

    public event Action<int, int> OnShieldChanged
    {
        add => _model.OnShieldChanged += value;
        remove => _model.OnShieldChanged -= value;
    }

    public void TakeDamage(int damage) => _model.TakeDamage(damage);
    public void AddHealth(int amount) => _model.AddHealth(amount);
    public void AddShield(int amount) => _model.AddShield(amount);
}

public interface IPlayerHealthInfoProvider
{
    int CurrentHealth { get; }
    int CurrentShield { get; }
}

public interface IPlayerHealthEventsProvider
{
    event Action<int, int> OnHealthChanged;
    event Action<int, int> OnShieldChanged;
}

public interface IPlayerHealthProvider
{
    void TakeDamage(int damage);
    void AddHealth(int amount);
    void AddShield(int amount);
}
