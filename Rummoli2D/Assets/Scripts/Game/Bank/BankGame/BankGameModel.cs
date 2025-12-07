using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankGameModel
{
    private readonly IDoorCounterEventsProvider _doorCounterEventsProvider;
    private readonly IMoneyProvider _moneyProvider;

    private int _currentMoney;

    public BankGameModel(IDoorCounterEventsProvider doorCounterEventsProvider, IMoneyProvider moneyProvider)
    {
        _doorCounterEventsProvider = doorCounterEventsProvider;
        _moneyProvider = moneyProvider;

        _doorCounterEventsProvider.OnCountChanged += SetOpenDoors;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _doorCounterEventsProvider.OnCountChanged -= SetOpenDoors;
    }

    private void SetOpenDoors(int count)
    {
        _currentMoney = CalculateCoins(count);

        OnChangeMoney?.Invoke(_currentMoney);

        Debug.Log("WIN: " + _currentMoney);
    }

    public void ApplyLoseBonus()
    {
        _moneyProvider.SendMoney(_currentMoney);

        OnApplyMoney?.Invoke();
    }

    public void ApplyWinBonus()
    {
        _moneyProvider.SendMoney(10000);

        OnApplyMoney?.Invoke();
    }

    private int CalculateCoins(int rooms)
    {
        int coins = 0;

        int tier1 = Mathf.Min(rooms, 20);
        coins += tier1 * 10;

        if (rooms > 20)
        {
            int tier2 = Mathf.Min(rooms - 20, 30);
            coins += tier2 * 20;
        }

        if (rooms > 50)
        {
            int tier3 = rooms - 50;
            coins += tier3 * 50;
        }

        return coins;
    }

    #region Output

    public event Action<int> OnChangeMoney;

    public event Action OnApplyMoney;

    #endregion
}
