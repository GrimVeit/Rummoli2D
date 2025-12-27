using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageChangeModel
{
    private readonly IStoreLanguageProvider _storeTextTranslateProvider;
    private readonly IStoreLanguageInfoProvider _storeTextTranslateInfoProvider;

    public LanguageChangeModel(IStoreLanguageProvider storeTextTranslateProvider, IStoreLanguageInfoProvider storeTextTranslateInfoProvider)
    {
        _storeTextTranslateProvider = storeTextTranslateProvider;
        _storeTextTranslateInfoProvider = storeTextTranslateInfoProvider;
    }

    public void Initialize()
    {
        OnSetLanguage?.Invoke(_storeTextTranslateInfoProvider.CurrentLanguage);
    }

    public void Dispose()
    {

    }

    public void ChooseLanguage(Language language)
    {
        _storeTextTranslateProvider.SetLanguage(language);
    }

    #region Output

    public event Action<Language> OnSetLanguage;

    #endregion
}
