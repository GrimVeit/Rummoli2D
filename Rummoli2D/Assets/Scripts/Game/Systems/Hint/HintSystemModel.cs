using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintSystemModel
{
    private readonly IStoreLanguageInfoProvider _storeLanguageInfoProvider;

    public HintSystemModel(IStoreLanguageInfoProvider storeLanguageInfoProvider)
    {
        _storeLanguageInfoProvider = storeLanguageInfoProvider;
    }

    public void Show(string key)
    {
        OnShow?.Invoke(key, _storeLanguageInfoProvider.CurrentLanguage);
    }

    public void Hide(string key)
    {
        OnHide?.Invoke(key);
    }

    public void ShowAll()
    {
        OnShowAll?.Invoke();
    }

    public void HideAll()
    {
        OnHideAll?.Invoke();
    }

    public void DeleteAll()
    {
        OnDeleteAll?.Invoke();
    }

    #region Output

    public event Action<string, Language> OnShow;
    public event Action<string> OnHide;
    public event Action OnShowAll;
    public event Action OnHideAll;
    public event Action OnDeleteAll;

    #endregion
}
