using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterPassPlayerSystemModel
{
    public int CurrentPassCount => _currentPassCount;
    public int TotalPlayerCount => _totalPlayerCount;
    public bool AllPassed => _currentPassCount >= _totalPlayerCount;


    private int _currentPassCount = 0;
    private int _totalPlayerCount = 0;

    private readonly IStoreLanguageInfoProvider _languageInfoProvider;

    public CounterPassPlayerSystemModel(IStoreLanguageInfoProvider languageInfoProvider)
    {
        _languageInfoProvider = languageInfoProvider;
    }

    public void SetTotalPlayers(int totalPlayerCount)
    {
        _totalPlayerCount = totalPlayerCount;
    }

    public void AddPass()
    {
        _currentPassCount += 1;
        OnChangeNamePassCount?.Invoke(NameLanguageUtility.GetPassCountName(_currentPassCount, _totalPlayerCount, _languageInfoProvider.CurrentLanguage));
    }

    public void Reset()
    {
        _currentPassCount = 0;
        OnChangeNamePassCount?.Invoke(NameLanguageUtility.GetPassCountName(_currentPassCount, _totalPlayerCount, _languageInfoProvider.CurrentLanguage));
    }

    #region Output

    public event Action<string> OnChangeNamePassCount;

    #endregion
}
