using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetSystemPresenter : IBetSystemInteractiveActivatorProvider, IBetSystemInfoProvider, IBetSystemEventsProvider, IBetSystemProvider
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
        _view.OnAddBet += _model.AddBet;
        _view.OnReturnBet += _model.ReturnBet;

        _model.OnStartAddBet += _view.StartAddBet;
        _model.OnStartReturnBet += _view.StartReturnBet;
        _model.OnSectorChangeCountBet += _view.SetSectorBetCount;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseSector -= _model.ChooseBet;
        _view.OnAddBet -= _model.AddBet;
        _view.OnReturnBet -= _model.ReturnBet;

        _model.OnStartAddBet -= _view.StartAddBet;
        _model.OnStartReturnBet -= _view.StartReturnBet;
        _model.OnSectorChangeCountBet -= _view.SetSectorBetCount;
    }

    #region Output 

    public event Action<int, int> OnStartAddBet
    {
        add => _model.OnStartAddBet += value;
        remove => _model.OnStartAddBet -= value;
    }

    public event Action<int, int> OnAddBet
    {
        add => _model.OnAddBet += value;
        remove => _model.OnAddBet -= value;
    }



    public event Action<int, int> OnReturnBet
    {
        add => _model.OnReturnBet += value;
        remove => _model.OnReturnBet -= value;
    }



    public event Action<int> OnPlayerBetCompleted
    {
        add => _model.OnPlayerBetCompleted += value;
        remove => _model.OnPlayerBetCompleted -= value;
    }

    #endregion

    #region Input

    public void StartAddBet(int playerIndex, int sectorIndex) => _model.StartAddBet(playerIndex, sectorIndex);
    public void StartReturnBet(int playerIndex, int sectorIndex) => _model.StartReturnBet(playerIndex, sectorIndex);

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
    public event Action<int, int> OnStartAddBet;
    public event Action<int, int> OnAddBet;

    public event Action<int, int> OnReturnBet;

    public event Action<int> OnPlayerBetCompleted;
}

public interface IBetSystemProvider
{
    public void StartAddBet(int playerIndex, int sectorIndex);
    public void StartReturnBet(int playerIndex, int sectorIndex);
}
