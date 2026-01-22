using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPeopleInputPresenter : IPlayerPeopleInputActivatorProvider, IPlayerPeopleInputEventsProvider
{
    private readonly PlayerPeopleInputModel _model;
    private readonly PlayerPeopleInputView _view;

    public PlayerPeopleInputPresenter(PlayerPeopleInputModel model, PlayerPeopleInputView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnChoose += _model.Choose;
        _view.OnPass += _model.Pass;
    }

    private void DeactivateEvents()
    {
        _view.OnChoose -= _model.Choose;
        _view.OnPass -= _model.Pass;
    }

    #region Output

    public event Action OnChoose
    {
        add => _model.OnChoose += value;
        remove => _model.OnChoose -= value;
    }

    public event Action OnPass
    {
        add => _model.OnPass += value;
        remove => _model.OnPass -= value;
    }

    #endregion

    #region Input

    public void ActivateChoose() => _view.ActivateChoose();
    public void DeactivateChoose() => _view.DeactivateChoose();

    public void ActivatePass() => _view.ActivatePass();
    public void DeactivatePass() => _view.DeactivatePass();

    public void SetMainChoose() => _view.SetMainChoose();
    public void SetMainPass() => _view.SetMainPass();

    #endregion
}

public interface IPlayerPeopleInputActivatorProvider
{
    public void ActivateChoose();
    public void DeactivateChoose();

    public void ActivatePass();
    public void DeactivatePass();

    public void SetMainChoose();
    public void SetMainPass();
}

public interface IPlayerPeopleInputEventsProvider
{
    public event Action OnChoose;
    public event Action OnPass;
}
