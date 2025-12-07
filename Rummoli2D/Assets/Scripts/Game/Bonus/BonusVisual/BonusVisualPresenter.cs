using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusVisualPresenter : IBonusVisualActivatorProvider, IBonusVisualEventsProvider
{
    private readonly BonusVisualModel _model;
    private readonly BonusVisualView _view;

    public BonusVisualPresenter(BonusVisualModel model, BonusVisualView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnChooseBonus += _model.ChooseBonus;

        _model.OnActivateInteraction += _view.ActivateInteraction;
        _model.OnDeactivateInteraction += _view.DeactivateInteraction;
        _model.OnChangedBonusCount += _view.SetBonusCount;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseBonus -= _model.ChooseBonus;

        _model.OnActivateInteraction -= _view.ActivateInteraction;
        _model.OnDeactivateInteraction -= _view.DeactivateInteraction;
        _model.OnChangedBonusCount -= _view.SetBonusCount;
    }

    #region Output

    public event Action<BonusType> OnChooseBonus_Value
    {
        add => _model.OnChooseBonus_Value += value;
        remove => _model.OnChooseBonus_Value -= value;
    }

    public event Action OnChooseBonus
    {
        add => _model.OnChooseBonus += value;
        remove => _model.OnChooseBonus -= value;
    }

    #endregion

    #region Input

    public void ActivateInteraction() => _model.ActivateInteraction();
    public void DeactivateInteraction() => _model.DeactivateInteraction();

    #endregion
}

public interface IBonusVisualActivatorProvider
{
    public void ActivateInteraction();
    public void DeactivateInteraction();
}

public interface IBonusVisualEventsProvider
{
    public event Action<BonusType> OnChooseBonus_Value;
    public event Action OnChooseBonus;
}
