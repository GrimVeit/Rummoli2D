using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPeopleInputPresenter : IPlayerPeopleInputActivatorProvider, IPlayerPeopleInputEventsProvider
{
    private readonly PlayerPeopleInputView _view;

    public PlayerPeopleInputPresenter(PlayerPeopleInputView view)
    {
        _view = view;
    }

    public void Initialize()
    {
        _view.Initialize();
    }

    public void Dispose()
    {
        _view.Dispose();
    }

    #region Output

    public event Action OnChoose
    {
        add => _view.OnChoose += value;
        remove => _view.OnChoose -= value;
    }

    public event Action OnPass
    {
        add => _view.OnPass += value;
        remove => _view.OnPass -= value;
    }

    #endregion

    #region Input

    public void ActivateChoose() => _view.ActivateChoose();
    public void DeactivateChoose() => _view.DeactivateChoose();

    public void ActivatePass() => _view.ActivatePass();
    public void DeactivatePass() => _view.DeactivatePass();

    #endregion
}

public interface IPlayerPeopleInputActivatorProvider
{
    public void ActivateChoose();
    public void DeactivateChoose();

    public void ActivatePass();
    public void DeactivatePass();
}

public interface IPlayerPeopleInputEventsProvider
{
    public event Action OnChoose;
    public event Action OnPass;
}
