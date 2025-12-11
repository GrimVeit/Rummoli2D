using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTranslateChangeModel
{
    private readonly IStoreTextTranslateProvider _storeTextTranslateProvider;
    private readonly IStoreTextTranslateInfoProvider _storeTextTranslateInfoProvider;

    public TextTranslateChangeModel(IStoreTextTranslateProvider storeTextTranslateProvider, IStoreTextTranslateInfoProvider storeTextTranslateInfoProvider)
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
