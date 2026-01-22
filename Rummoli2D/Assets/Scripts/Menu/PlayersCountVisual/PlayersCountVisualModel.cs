using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersCountVisualModel
{
    private readonly IStorePlayersCountInfoProvider _playersCountInfoProvider;
    private readonly IStorePlayersCountListener _playersCountListener;
    private readonly IStorePlayersCountProvider _playersCountProvider;
    private readonly ISoundProvider _soundProvider;

    private int _currentCount = -1;

    public PlayersCountVisualModel(IStorePlayersCountInfoProvider playersCountInfoProvider, IStorePlayersCountListener playersCountListener, IStorePlayersCountProvider playersCountProvider, ISoundProvider soundProvider)
    {
        _playersCountInfoProvider = playersCountInfoProvider;
        _playersCountListener = playersCountListener;
        _playersCountProvider = playersCountProvider;
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        _currentCount = _playersCountInfoProvider.PlayersCount;
        OnPlayersCountChanged?.Invoke(_currentCount);

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
        if (_currentCount == count) return;

        _currentCount = count;

        _soundProvider.PlayOneShot("ChooseRoundPlayers");

        OnPlayersCountChanged?.Invoke(count);
    }

    #endregion
}
