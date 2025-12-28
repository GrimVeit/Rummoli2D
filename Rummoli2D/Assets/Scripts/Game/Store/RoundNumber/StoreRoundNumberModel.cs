using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreRoundNumberModel
{
    public int RoundNumber => _roundNumber;

    private int _roundNumber;

    public void Initialize()
    {
        _roundNumber = 0;
        OnRoundNumberChanged?.Invoke(_roundNumber);
    }

    public void AddRound()
    {
        _roundNumber += 1;
        OnRoundNumberChanged?.Invoke(_roundNumber);
    }

    public void Dispose()
    {

    }

    #region Output

    public event Action<int> OnRoundNumberChanged;

    #endregion
}
