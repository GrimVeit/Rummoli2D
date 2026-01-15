using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePlayersCountModel
{
    public int PlayersCount => _playersCount;

    private const int _MinPlayersCount = 2;
    private const int _MaxPlayersCount = 5;

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
        if(count < _MinPlayersCount || count > _MaxPlayersCount)
        {
            _playersCount = _MinPlayersCount;
        }

        OnPlayersCountChanged?.Invoke(_playersCount);
    }

    public void IncreasePlayersCount()
    {
        _playersCount += 1;

        if(_playersCount > _MaxPlayersCount)
        {
            _playersCount = _MaxPlayersCount;
        }

        OnPlayersCountChanged?.Invoke(_playersCount);
    }

    public void DecreasePlayersCount()
    {
        _playersCount -= 1;

        if (_playersCount < _MinPlayersCount)
        {
            _playersCount = _MinPlayersCount;
        }

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
