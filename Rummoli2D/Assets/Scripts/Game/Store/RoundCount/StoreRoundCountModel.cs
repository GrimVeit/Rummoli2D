using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreRoundCountModel
{
    public int RoundsCount => _roundsCount;

    private const int _MinRoundsCount = 1;
    private const int _MaxRoundsCount = 10;

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
        if (count < _MinRoundsCount || count > _MaxRoundsCount)
        {
            _roundsCount = _MinRoundsCount;
        }

        OnRoundsCountChanged?.Invoke(_roundsCount);
    }

    public void IncreaseRoundsCount()
    {
        _roundsCount += 1;

        if (_roundsCount > _MaxRoundsCount)
        {
            _roundsCount = _MaxRoundsCount;
        }

        OnRoundsCountChanged?.Invoke(_roundsCount);
    }

    public void DecreaseRoundsCount()
    {
        _roundsCount -= 1;

        if (_roundsCount < _MinRoundsCount)
        {
            _roundsCount = _MinRoundsCount;
        }

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
