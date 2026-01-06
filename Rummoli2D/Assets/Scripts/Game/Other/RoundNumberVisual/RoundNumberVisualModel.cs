using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundNumberVisualModel
{
    private readonly IStoreRoundNumberInfoProvider _roundNumberInfoProvider;
    private readonly IStoreRoundNumberListener _roundNumberListener;
    private readonly IStoreLanguageInfoProvider _languageInfoProvider;
    private readonly IStoreLanguageListener _languageListener;

    private int _roundNumber;

    public RoundNumberVisualModel(IStoreRoundNumberInfoProvider storeRoundNumberInfoProvider, IStoreRoundNumberListener storeRoundNumberListener, IStoreLanguageInfoProvider storeLanguageInfoProvider, IStoreLanguageListener storeLanguageListener)
    {
        _roundNumberInfoProvider = storeRoundNumberInfoProvider;
        _roundNumberListener = storeRoundNumberListener;
        _languageInfoProvider = storeLanguageInfoProvider;
        _languageListener = storeLanguageListener;
    }

    public void Initialize()
    {
        _roundNumberListener.OnRoundNumberChanged += ChangeRoundNumber;
        _languageListener.OnChangeLanguage += ChangeLanguage;

        ChangeRoundNumber(_roundNumberInfoProvider.RoundNumber);
    }

    public void Dispose()
    {
        _roundNumberListener.OnRoundNumberChanged -= ChangeRoundNumber;
        _languageListener.OnChangeLanguage -= ChangeLanguage;
    }

    private void ChangeRoundNumber(int number)
    {
        _roundNumber = number;

        OnChangeRoundName_Open?.Invoke(NameLanguageUtility.GetRoundName_Open(_roundNumber, _languageInfoProvider.CurrentLanguage));
        OnChangeRoundName_Completed?.Invoke(NameLanguageUtility.GetRoundName_Complete(_roundNumber, _languageInfoProvider.CurrentLanguage));
    }

    private void ChangeLanguage(Language language)
    {
        OnChangeRoundName_Open?.Invoke(NameLanguageUtility.GetRoundName_Open(_roundNumber, language));
        OnChangeRoundName_Completed?.Invoke(NameLanguageUtility.GetRoundName_Complete(_roundNumber, language));
    }

    #region Output

    public event Action<string> OnChangeRoundName_Open;
    public event Action<string> OnChangeRoundName_Completed;

    #endregion
}
