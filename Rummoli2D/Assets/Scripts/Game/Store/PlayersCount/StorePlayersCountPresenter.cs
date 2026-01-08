using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePlayersCountPresenter : IStorePlayersCountProvider, IStorePlayersCountInfoProvider, IStorePlayersCountListener
{
    private readonly StorePlayersCountModel _model;

    public StorePlayersCountPresenter(StorePlayersCountModel model)
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

    public event Action<int> OnPlayersCountChanged
    {
        add => _model.OnPlayersCountChanged += value;
        remove => _model.OnPlayersCountChanged -= value;
    }

    #endregion

    #region Input

    public int PlayersCount => _model.PlayersCount;
    public void SetPlayersCount(int count) => _model.SetPlayersCount(count);

    #endregion
}

public interface IStorePlayersCountProvider
{
    public void SetPlayersCount(int count);
}

public interface IStorePlayersCountInfoProvider
{
    public int PlayersCount { get; }
}

public interface IStorePlayersCountListener
{
    public event Action<int> OnPlayersCountChanged;
}
