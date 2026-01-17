using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreProgressScorePresenter : IStoreProgressScoreProvider, IStoreProgressScoreInfoProvider, IStoreProgressScoretListener
{
    private readonly StoreProgressScoreModel _model;

    public StoreProgressScorePresenter(StoreProgressScoreModel model)
    {
        _model = model;
    }

    public void Initialize()
    {
        _model.Initialize();
    }

    public void Dispose()
    {
        _model.Dispose();
    }

    #region Output

    public event Action<int> OnScoreProgressChanged
    {
        add => _model.OnScoreProgressChanged += value;
        remove => _model.OnScoreProgressChanged -= value;
    }

    #endregion

    #region Input

    public int ScoreProgress => _model.ScoreProgress;
    public void ResetScoreProgress() => _model.ResetScoreProgress();
    public void AddScoreProgress(int score) => _model.AddScoreProgress(score);

    #endregion
}

public interface IStoreProgressScoreProvider
{
    public void ResetScoreProgress();
    public void AddScoreProgress(int score);
}

public interface IStoreProgressScoreInfoProvider
{
    public int ScoreProgress { get; }
}

public interface IStoreProgressScoretListener
{
    public event Action<int> OnScoreProgressChanged;
}
