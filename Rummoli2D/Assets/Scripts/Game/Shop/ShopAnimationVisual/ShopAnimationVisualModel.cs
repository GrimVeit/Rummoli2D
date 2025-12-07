using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAnimationVisualModel
{
    private readonly IStoreShopEventsProvider _storeShopEventsProvider;

    public ShopAnimationVisualModel(IStoreShopEventsProvider storeShopEventsProvider)
    {
        _storeShopEventsProvider = storeShopEventsProvider;

        _storeShopEventsProvider.OnLevelShopChanged += SetLevelShopChanged;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _storeShopEventsProvider.OnLevelShopChanged -= SetLevelShopChanged;
    }

    #region Output

    public event Action<ShopGroup, int> OnLevelShopChanged;

    private void SetLevelShopChanged(ShopGroup group, int levelId)
    {
        OnLevelShopChanged?.Invoke(group, levelId);
    }

    #endregion
}
