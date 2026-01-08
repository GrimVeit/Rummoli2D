using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreRoundCurrentNumberPresenter : IStoreRoundCurrentNumberListener, IStoreRoundCurrentNumberInfoProvider, IStoreRoundCurrentNumberProvider
{
    private readonly StoreRoundCurrentNumberModel _model;

    public StoreRoundCurrentNumberPresenter(StoreRoundCurrentNumberModel model)
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

    public int RoundCurrentNumber => _model.RoundNumber;

    public void AddRound() => _model.AddRound();

    #endregion
}

public interface IStoreRoundCurrentNumberListener
{
    public event Action<int> OnRoundNumberChanged;
}

public interface IStoreRoundCurrentNumberInfoProvider
{
    public int RoundCurrentNumber { get; }
}

public interface IStoreRoundCurrentNumberProvider
{
    public void AddRound();
}
