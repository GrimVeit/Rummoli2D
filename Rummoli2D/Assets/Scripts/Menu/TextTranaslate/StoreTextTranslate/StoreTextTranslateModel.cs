using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreTextTranslateModel
{
    public Language CurrentLanguage => _currentLanguage;

    private readonly Language _defLanguage = Language.English;

    private Language _currentLanguage;
    private readonly string KEY;

    public StoreTextTranslateModel(string key)
    {
        KEY = key;
    }

    public void Initialize()
    {
        _currentLanguage = (Language)PlayerPrefs.GetInt(KEY, (int)_defLanguage);

        OnChangeLanguage?.Invoke(_currentLanguage);
    }

    public void Dispose()
    {
        PlayerPrefs.SetInt(KEY, (int)_currentLanguage);
    }

    #region Input

    public void SetLanguage(Language language)
    {
        _currentLanguage = language;

        OnChangeLanguage?.Invoke(_currentLanguage);
    }

    #endregion

    #region Output

    public event Action<Language> OnChangeLanguage;

    #endregion
}

public enum Language
{
    English = 0,
    Russia = 1,
}
