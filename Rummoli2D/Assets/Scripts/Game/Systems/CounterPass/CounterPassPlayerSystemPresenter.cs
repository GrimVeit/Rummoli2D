using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterPassPlayerSystemPresenter : ICounterPassPlayerSystemProvider, ICounterPassPlayerSystemActivatorProvider
{
    private readonly CounterPassPlayerSystemModel _model;
    private readonly CounterPassPlayerSystemView _view;

    public CounterPassPlayerSystemPresenter(CounterPassPlayerSystemModel model, CounterPassPlayerSystemView view)
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
        _model.OnChangeNamePassCount += _view.SetPassName;
    }

    private void DeactivateEvents()
    {
        _model.OnChangeNamePassCount += _view.SetPassName;
    }

    #region Input

    public int CurrentPassCount => _model.CurrentPassCount;
    public int TotalPlayerCount => _model.TotalPlayerCount;
    public bool AllPassed => _model.AllPassed;

    public void SetTotalPlayers(int totalPlayerCount) => _model.SetTotalPlayers(totalPlayerCount);
    public void AddPass() => _model.AddPass();
    public void Reset() => _model.Reset();


    public void ActivateVisual(Action OnComplete) => _view.ActivateVisual(OnComplete);
    public void DeactivateVisual(Action OnComplete) => _view.DeactivateVisual(OnComplete);

    #endregion
}

public interface ICounterPassPlayerSystemProvider
{
    public int CurrentPassCount { get; }
    public int TotalPlayerCount { get; }
    public bool AllPassed { get; }

    public void SetTotalPlayers(int totalPlayerCount);
    public void AddPass();
    public void Reset();
}

public interface ICounterPassPlayerSystemActivatorProvider
{
    void ActivateVisual(Action OnComplete = null);
    void DeactivateVisual(Action OnComplete = null);
}
