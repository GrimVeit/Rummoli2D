using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreRoundCountModel
{
    public int RoundsCount => _roundsCount;

    private readonly string _key;
    private int _roundsCount;

    public StoreRoundCountModel(string key)
    {
        _key = key;
    }

    public void Initialize()
    {
        _roundsCount = PlayerPrefs.GetInt(_key, 3);

        if (_roundsCount < 1 || _roundsCount > 10)
            _roundsCount = 3;

        OnRoundsCountChanged?.Invoke(_roundsCount);
    }

    public void SetRoundsCount(int count)
    {
        _roundsCount = count;
        OnRoundsCountChanged?.Invoke(_roundsCount);
    }

    public void Dispose()
    {
        PlayerPrefs.SetInt(_key, _roundsCount);
        PlayerPrefs.Save();
    }

    #region Output

    public event Action<int> OnRoundsCountChanged;

    #endregion
}
