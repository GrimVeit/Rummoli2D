using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePlayerPresenter : IScorePlayerProvider
{
    private readonly ScorePlayerModel _model;
    private readonly ScorePlayerView _view;

    public ScorePlayerPresenter(ScorePlayerModel model, ScorePlayerView view)
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
        _model.OnRemoveScore += _view.RemoveScore;
        _model.OnAddScore += _view.AddScore;
    }

    private void DeactivateEvents()
    {
        _model.OnRemoveScore -= _view.RemoveScore;
        _model.OnAddScore -= _view.AddScore;
    }

    #region Output

    public event Action<int> OnAddScore
    {
        add => _model.OnAddScore += value;
        remove => _model.OnAddScore -= value;
    }

    public event Action<int> OnRemoveScore
    {
        add => _model.OnRemoveScore += value;
        remove => _model.OnRemoveScore -= value;
    }

    #endregion

    #region Input

    public int TotalScore => _model.TotalScore;
    public int TotalEarnedScore => _model.TotalEarnedScore;

    public void SetScore(int score) => _model.SetScore(score);
    public void AddScore(int score) => _model.AddScore(score);
    public void RemoveScore() => _model.RemoveScore();

    #endregion
}

public interface IScorePlayerProvider
{
    public void SetScore(int score);
    public void AddScore(int score);
    public void RemoveScore();
}

public interface IScoreInfoProvider
{
    public int TotalScore { get; }
    public int TotalEarnedScore { get; }
}

public interface IScorePlayerListener
{
    public event Action<int> OnAddScore;
    public event Action<int> OnRemoveScore;
}
