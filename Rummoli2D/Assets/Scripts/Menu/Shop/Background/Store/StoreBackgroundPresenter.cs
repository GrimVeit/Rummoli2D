using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreBackgroundPresenter : IStoreBackgroundEventsProvider, IStoreBackgroundInfoProvider, IStoreBackgroundProvider
{
    private readonly StoreBackgroundModel _model;

    public StoreBackgroundPresenter(StoreBackgroundModel model)
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

    public event Action<int> OnOpenBackground
    {
        add => _model.OnOpenBackground += value;
        remove => _model.OnOpenBackground -= value;
    }

    public event Action<int> OnCloseBackground
    {
        add => _model.OnCloseBackground += value;
        remove => _model.OnCloseBackground -= value;
    }


    public event Action<int> OnDeselectBackground
    {
        add => _model.OnDeselectBackground += value;
        remove => _model.OnDeselectBackground -= value;
    }

    public event Action<int> OnSelectBackground
    {
        add => _model.OnSelectBackground += value;
        remove => _model.OnSelectBackground -= value;
    }

    #endregion

    #region Input

    public void OpenBackground(int idBackground, Action OnComplete) => _model.OpenBackground(idBackground, OnComplete);

    public void SelectBackground(int idBackground, Action OnComplete) => _model.SelectBackground(idBackground, OnComplete);

    public int GetBackgroundIndex() => _model.GetBackgroundIndex();
    public BackgroundData GetBackgroundData(int id) => _model.GetBackgroundData(id);


    #endregion
}

public interface IStoreBackgroundEventsProvider
{
    public event Action<int> OnOpenBackground;
    public event Action<int> OnCloseBackground;

    public event Action<int> OnDeselectBackground;
    public event Action<int> OnSelectBackground;
}

public interface IStoreBackgroundInfoProvider
{
    public int GetBackgroundIndex();
    public BackgroundData GetBackgroundData(int id);
}

public interface IStoreBackgroundProvider
{
    public void OpenBackground(int idBackground, Action OnComplete = null);

    public void SelectBackground(int idBackground, Action OnComplete = null);
}
