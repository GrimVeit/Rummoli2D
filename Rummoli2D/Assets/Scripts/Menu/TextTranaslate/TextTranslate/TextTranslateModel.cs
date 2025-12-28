using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTranslateModel
{
    private readonly IStoreLanguageListener _storeTextTranslateEventsProvider;
    private readonly IStoreLanguageInfoProvider _storeTextTranslateInfoProvider;

    public TextTranslateModel(IStoreLanguageListener storeTextTranslateEventsProvider, IStoreLanguageInfoProvider storeTextTranslateInfoProvider)
    {
        _storeTextTranslateEventsProvider = storeTextTranslateEventsProvider;
        _storeTextTranslateInfoProvider = storeTextTranslateInfoProvider;
    }

    public void Initialize()
    {
        _storeTextTranslateEventsProvider.OnChangeLanguage += SetLanguage;

        OnChangeLanguage?.Invoke(_storeTextTranslateInfoProvider.CurrentLanguage, false);
    }

    public void Dispose()
    {
        _storeTextTranslateEventsProvider.OnChangeLanguage -= SetLanguage;
    }

    private void SetLanguage(Language language)
    {
        OnChangeLanguage?.Invoke(language, true);
    }

    #region Output

    public event Action<Language, bool> OnChangeLanguage;

    #endregion
}
