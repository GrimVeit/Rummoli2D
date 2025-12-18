using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetSystemPresenter : IBetSystemInteractiveActivatorProvider
{
    private readonly BetSystemModel _model;
    private readonly BetSystemView _view;

    public BetSystemPresenter(BetSystemModel model, BetSystemView view)
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
        _view.OnChooseSector += _model.ChooseBet;
        _view.OnSubmitBet += _model.SubmitBet;

        _model.OnAddBet += _view.AddBet;
        _model.OnSectorChangeCountBet += _view.SetSectorBetCount;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseSector -= _model.ChooseBet;
        _view.OnSubmitBet -= _model.SubmitBet;

        _model.OnAddBet -= _view.AddBet;
        _model.OnSectorChangeCountBet -= _view.SetSectorBetCount;
    }

    #region Output 

    public event Action<int> OnPlayerBetCompleted
    {
        add => _model.OnPlayerBetCompleted += value;
        remove => _model.OnPlayerBetCompleted -= value;
    }

    #endregion

    #region Input

    public void AddBet(int playerIndex, int sectorIndex) => _model.AddBet(playerIndex, sectorIndex);

    public void ActivateInteractive() => _view.ActivateInteractive();
    public void DeactivateInteractive() => _view.DeactivateInteractive();

    public bool IsPlayerBetCompleted(int playerIndex) => _model.IsPlayerBetCompleted(playerIndex);
    public bool TryGetRandomAvailableSector(int playerIndex, out int sectorIndex) => _model.TryGetRandomAvailableSector(playerIndex, out sectorIndex);

    #endregion
}

public interface IBetSystemInteractiveActivatorProvider
{
    public void ActivateInteractive();
    public void DeactivateInteractive();
}

public interface IBetSystemInfoProvider
{
    public bool IsPlayerBetCompleted(int playerIndex);
    public bool TryGetRandomAvailableSector(int playerIndex, out int sectorIndex);
}

public interface IBetSystemEventsProvider
{
    public event Action<int> OnPlayerBetCompleted;
}

public interface IBetSystemProvider
{
    public void AddBet(int playerIndex, int sectorIndex);
}
