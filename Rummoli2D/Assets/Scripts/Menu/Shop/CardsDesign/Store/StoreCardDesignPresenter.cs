using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreCardDesignPresenter : IStoreCardDesignEventsProvider, IStoreCardDesignInfoProvider, IStoreCardDesignProvider
{
    private readonly StoreCardDesignModel _model;

    public StoreCardDesignPresenter(StoreCardDesignModel model)
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

    public event Action<int> OnOpenDesign
    {
        add => _model.OnOpenDesign += value;
        remove => _model.OnOpenDesign -= value;
    }

    public event Action<int> OnCloseDesign
    {
        add => _model.OnCloseDesign += value;
        remove => _model.OnCloseDesign -= value;
    }


    public event Action<int> OnDeselectDesign
    {
        add => _model.OnDeselectDesign += value;
        remove => _model.OnDeselectDesign -= value;
    }

    public event Action<int> OnSelectDesign
    {
        add => _model.OnSelectDesign += value;
        remove => _model.OnSelectDesign -= value;
    }

    #endregion

    #region Input

    public void OpenDesign(int id, Action OnComplete) => _model.OpenDesign(id, OnComplete);

    public void SelectDesign(int id, Action OnComplete) => _model.SelectDesign(id, OnComplete);

    public int GetCardDesignIndex() => _model.GetDesignIndex();
    public CardDesignData GetCardDesignData(int id) => _model.GetCardDesignData(id);


    #endregion
}

public interface IStoreCardDesignEventsProvider
{
    public event Action<int> OnOpenDesign;
    public event Action<int> OnCloseDesign;

    public event Action<int> OnDeselectDesign;
    public event Action<int> OnSelectDesign;
}

public interface IStoreCardDesignInfoProvider
{
    public int GetCardDesignIndex();
    public CardDesignData GetCardDesignData(int id);
}

public interface IStoreCardDesignProvider
{
    public void OpenDesign(int id, Action OnComplete = null);

    public void SelectDesign(int id, Action OnComplete = null);
}
