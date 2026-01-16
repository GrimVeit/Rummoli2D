using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEarnLeaderboardPresenter : IScoreEarnLeaderboardProvider, IScoreEarnWinnerProvider, IScoreEarnWinnerListener
{
    private readonly ScoreEarnLeaderboardModel _model;
    private readonly ScoreEarnLeaderboardView _view;

    public ScoreEarnLeaderboardPresenter(ScoreEarnLeaderboardModel model, ScoreEarnLeaderboardView view)
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
        _model.OnClearPlayers += _view.ClearAll;
        _model.OnRegisterPlayers += _view.RegisterPlayers;
        _model.OnAddScore += _view.UpdateScore;
        _model.OnSetCoins += _view.SetCoins;
    }

    private void DeactivateEvents()
    {
        _model.OnClearPlayers -= _view.ClearAll;
        _model.OnRegisterPlayers -= _view.RegisterPlayers;
        _model.OnAddScore -= _view.UpdateScore;
        _model.OnSetCoins -= _view.SetCoins;
    }

    #region Output

    public event Action OnEndEarn
    {
        add => _view.OnEndSetCoins += value;
        remove => _view.OnEndSetCoins -= value;
    }

    #endregion

    #region Input

    public void RegisterPlayers(List<IPlayer> players) => _model.RegisterPlayers(players);
    public void SearchWinners() => _model.SearchWinners();

    #endregion
}

public interface IScoreEarnLeaderboardProvider
{
    public void RegisterPlayers(List<IPlayer> players);
}

public interface IScoreEarnWinnerProvider
{
    public void SearchWinners();
}

public interface IScoreEarnWinnerListener
{
    public event Action OnEndEarn;
}
