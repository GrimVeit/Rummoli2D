using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankGamePresenter : IBankGameProvider, IBankGameEventsProvider
{
    private readonly BankGameModel _model;
    private readonly BankGameView _view;

    public BankGamePresenter(BankGameModel model, BankGameView view)
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

        _model.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnChangeMoney += _view.SetMoney;
    }

    private void DeactivateEvents()
    {
        _model.OnChangeMoney -= _view.SetMoney;
    }

    #region Output

    public event Action OnApplyMoney
    {
        add => _model.OnApplyMoney += value;
        remove => _model.OnApplyMoney -= value;
    }

    #endregion

    #region Input

    public void ApplyLoseBonus() => _model.ApplyLoseBonus();
    public void ApplyWinBonus() => _model.ApplyWinBonus();

    #endregion
}

public interface IBankGameProvider
{
    public void ApplyLoseBonus();
    public void ApplyWinBonus();
}

public interface IBankGameEventsProvider
{
    public event Action OnApplyMoney;
}
