using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageChangeView : View
{
    [SerializeField] private DropdownLanguages dropdownLanguages;
    [SerializeField] private TMP_Dropdown dropdownLang;

    public void Initialize()
    {
        dropdownLang.onValueChanged.AddListener(ChooseLanguage);
    }

    public void Dispose()
    {
        dropdownLang.onValueChanged.RemoveListener(ChooseLanguage);
    }

    public void SetLanguage(Language language)
    {
        dropdownLang.value = dropdownLanguages.GetIndexLanguage(language);
    }

    private void ChooseLanguage(int index)
    {
        var language = dropdownLanguages.GetLanguage(index);
        OnChooseLanguage?.Invoke(language);
    }

    #region Output

    public event Action<Language> OnChooseLanguage;

    #endregion
}

[System.Serializable]
public class DropdownLanguages
{
    [SerializeField] private List<DropdownLanguage> languages = new();

    public Language GetLanguage(int index) => languages.Find(data => data.IndexLanguage == index).Language;
    public int GetIndexLanguage(Language language) => languages.Find(data => data.Language == language).IndexLanguage;
}

[System.Serializable]
public class DropdownLanguage
{
    [SerializeField] private Language language;
    [SerializeField] private int indexLanguage;

    public Language Language => language;
    public int IndexLanguage => indexLanguage;
}
