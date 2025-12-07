using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreShopPresenter : IStoreShopEventsProvider, IStoreShopProvider
{
    private readonly StoreShopModel _model;

    public StoreShopPresenter(StoreShopModel model)
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

    public event Action<ShopGroup, int> OnLevelShopChanged
    {
        add => _model.OnLevelShopChanged += value;
        remove => _model.OnLevelShopChanged -= value;
    }

    #endregion

    #region Input

    public void SetLevel(ShopGroup group, int levelId) => _model.SetLevel(group, levelId);

    #endregion
}

public interface IStoreShopEventsProvider
{
    public event Action<ShopGroup, int> OnLevelShopChanged;
}

public interface IStoreShopProvider
{
    public void SetLevel(ShopGroup group, int levelId);
}
