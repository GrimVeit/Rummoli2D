using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreTextTranslatePresenter : IStoreTextTranslateInfoProvider, IStoreTextTranslateProvider, IStoreTextTranslateEventsProvider
{
    private readonly StoreTextTranslateModel _model;

    public StoreTextTranslatePresenter(StoreTextTranslateModel model)
    {
        _model = model;
    }

    public void Initialize()
    {
        _model.Initialize();
    }

    public void Dispose()
    {
        _model.Dispose();
    }

    #region Output

    public event Action<Language> OnChangeLanguage
    {
        add => _model.OnChangeLanguage += value;
        remove => _model.OnChangeLanguage -= value;
    }

    #endregion


    #region Input
    public void SetLanguage(Language language) => _model.SetLanguage(language);
    public Language CurrentLanguage => _model.CurrentLanguage;

    #endregion
}

public interface IStoreTextTranslateProvider
{
    public void SetLanguage(Language language);
}

public interface IStoreTextTranslateInfoProvider
{
    public Language CurrentLanguage { get; }
}

public interface IStoreTextTranslateEventsProvider
{
    public event Action<Language> OnChangeLanguage;
}
