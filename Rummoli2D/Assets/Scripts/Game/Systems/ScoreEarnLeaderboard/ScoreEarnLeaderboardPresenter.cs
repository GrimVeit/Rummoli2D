using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEarnLeaderboardPresenter : IScoreEarnLeaderboardProvider
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
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnClearPlayers += _view.ClearAll;
        _model.OnRegisterPlayers += _view.RegisterPlayers;
        _model.OnAddScore += _view.UpdateScore;
    }

    private void DeactivateEvents()
    {
        _model.OnClearPlayers -= _view.ClearAll;
        _model.OnRegisterPlayers -= _view.RegisterPlayers;
        _model.OnAddScore -= _view.UpdateScore;
    }

    #region Input

    public void RegisterPlayers(List<IPlayer> players) => _model.RegisterPlayers(players);

    #endregion
}

public interface IScoreEarnLeaderboardProvider
{
    public void RegisterPlayers(List<IPlayer> players);
}
