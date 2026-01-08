using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreRoundCountPresenter : IStoreRoundCountProvider, IStoreRoundCountInfoProvider, IStoreRoundCountListener
{
    private readonly StoreRoundCountModel _model;

    public StoreRoundCountPresenter(StoreRoundCountModel model)
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

    public event Action<int> OnRoundsCountChanged
    {
        add => _model.OnRoundsCountChanged += value;
        remove => _model.OnRoundsCountChanged -= value;
    }

    #endregion

    #region Input

    public int RoundsCount => _model.RoundsCount;
    public void SetRoundsCount(int count) => _model.SetRoundsCount(count);

    #endregion
}

public interface IStoreRoundCountProvider
{
    public void SetRoundsCount(int count);
}

public interface IStoreRoundCountInfoProvider
{
    public int RoundsCount { get; }
}

public interface IStoreRoundCountListener
{
    public event Action<int> OnRoundsCountChanged;
}
