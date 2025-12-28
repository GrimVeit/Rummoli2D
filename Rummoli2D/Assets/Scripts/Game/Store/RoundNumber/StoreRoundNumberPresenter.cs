using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreRoundNumberPresenter : IStoreRoundNumberListener, IStoreRoundNumberInfoProvider, IStoreRoundNumberProvider
{
    private readonly StoreRoundNumberModel _model;

    public StoreRoundNumberPresenter(StoreRoundNumberModel model)
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

    public event Action<int> OnRoundNumberChanged
    {
        add => _model.OnRoundNumberChanged += value;
        remove => _model.OnRoundNumberChanged -= value;
    }

    #endregion

    #region Input

    public int RoundNumber => _model.RoundNumber;

    public void AddRound() => _model.AddRound();

    #endregion
}

public interface IStoreRoundNumberListener
{
    public event Action<int> OnRoundNumberChanged;
}

public interface IStoreRoundNumberInfoProvider
{
    public int RoundNumber { get; }
}

public interface IStoreRoundNumberProvider
{
    public void AddRound();
}
