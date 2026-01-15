using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundCountVisualModel
{
    private readonly IStoreRoundCountInfoProvider _roundCountInfoProvider;
    private readonly IStoreRoundCountListener _roundCountListener;
    private readonly IStoreRoundCountProvider _roundCountProvider;

    public RoundCountVisualModel(IStoreRoundCountInfoProvider roundCountInfoProvider, IStoreRoundCountListener roundCountListener, IStoreRoundCountProvider roundCountProvider)
    {
        _roundCountInfoProvider = roundCountInfoProvider;
        _roundCountListener = roundCountListener;
        _roundCountProvider = roundCountProvider;
    }

    public void Initialize()
    {
        ChangeRoundsCount(_roundCountInfoProvider.RoundsCount);

        _roundCountListener.OnRoundsCountChanged += ChangeRoundsCount;
    }

    public void Dispose()
    {
        _roundCountListener.OnRoundsCountChanged -= ChangeRoundsCount;
    }

    public void IncreaseCount()
    {
        _roundCountProvider.IncreaseRoundsCount();
    }

    public void DecreaseCount()
    {
        _roundCountProvider.DecreaseRoundsCount();
    }

    #region Output

    public event Action<int> OnRoundsCountChanged;

    private void ChangeRoundsCount(int count)
    {
        OnRoundsCountChanged?.Invoke(count);
    }

    #endregion
}
