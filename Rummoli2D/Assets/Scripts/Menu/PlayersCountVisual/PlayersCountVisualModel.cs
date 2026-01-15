using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersCountVisualModel
{
    private readonly IStorePlayersCountInfoProvider _playersCountInfoProvider;
    private readonly IStorePlayersCountListener _playersCountListener;
    private readonly IStorePlayersCountProvider _playersCountProvider;

    public PlayersCountVisualModel(IStorePlayersCountInfoProvider playersCountInfoProvider, IStorePlayersCountListener playersCountListener, IStorePlayersCountProvider playersCountProvider)
    {
        _playersCountInfoProvider = playersCountInfoProvider;
        _playersCountListener = playersCountListener;
        _playersCountProvider = playersCountProvider;
    }

    public void Initialize()
    {
        ChangePlayersCount(_playersCountInfoProvider.PlayersCount);

        _playersCountListener.OnPlayersCountChanged += ChangePlayersCount;
    }

    public void Dispose()
    {
        _playersCountListener.OnPlayersCountChanged -= ChangePlayersCount;
    }

    public void IncreaseCount()
    {
        _playersCountProvider.IncreasePlayersCount();
    }

    public void DecreaseCount()
    {
        _playersCountProvider.DecreasePlayersCount();
    }

    #region Output

    public event Action<int> OnPlayersCountChanged;

    private void ChangePlayersCount(int count)
    {
        OnPlayersCountChanged?.Invoke(count);
    }

    #endregion
}
