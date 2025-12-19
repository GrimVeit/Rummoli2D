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
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        _model.OnChangeScore += _view.SetScore;
    }

    private void DeactivateEvents()
    {
        _model.OnChangeScore -= _view.SetScore;
    }

    #region Input

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
