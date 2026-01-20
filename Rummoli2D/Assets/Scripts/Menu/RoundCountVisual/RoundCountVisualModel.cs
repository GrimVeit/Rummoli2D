using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundCountVisualModel
{
    private readonly IStoreRoundCountInfoProvider _roundCountInfoProvider;
    private readonly IStoreRoundCountListener _roundCountListener;
    private readonly IStoreRoundCountProvider _roundCountProvider;
    private readonly ISoundProvider _soundProvider;

    private int _currentCount = -1;

    public RoundCountVisualModel(IStoreRoundCountInfoProvider roundCountInfoProvider, IStoreRoundCountListener roundCountListener, IStoreRoundCountProvider roundCountProvider, ISoundProvider soundProvider)
    {
        _roundCountInfoProvider = roundCountInfoProvider;
        _roundCountListener = roundCountListener;
        _roundCountProvider = roundCountProvider;
        _soundProvider = soundProvider;
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
        if(_currentCount == count) return;

        _currentCount = count;

        _soundProvider.PlayOneShot("ChooseRoundPlayers");

        OnRoundsCountChanged?.Invoke(_currentCount);
    }

    #endregion
}
