using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePlayersCountModel
{
    public int PlayersCount => _playersCount;

    private readonly string _key;
    private int _playersCount;

    public StorePlayersCountModel(string key)
    {
        _key = key;
    }

    public void Initialize()
    {
        _playersCount = PlayerPrefs.GetInt(_key, 2);

        if (_playersCount < 2 || _playersCount > 5)
            _playersCount = 2;

        OnPlayersCountChanged?.Invoke(_playersCount);
    }

    public void SetPlayersCount(int count)
    {
        _playersCount = count;
        OnPlayersCountChanged?.Invoke(_playersCount);
    }

    public void Dispose()
    {
        PlayerPrefs.SetInt(_key, _playersCount);
        PlayerPrefs.Save();
    }

    #region Output

    public event Action<int> OnPlayersCountChanged;

    #endregion
}
