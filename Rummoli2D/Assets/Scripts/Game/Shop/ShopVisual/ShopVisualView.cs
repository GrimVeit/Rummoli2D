using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopVisualView : View
{
    [SerializeField] private List<ShopVisualGroup> shopVisualGroups = new();

    public void Initialize()
    {
        shopVisualGroups.ForEach(group =>
        {
            group.OnBuy += Buy;
            group.Initialize();
        });
    }

    public void Dispose()
    {
        shopVisualGroups.ForEach(group =>
        {
            group.OnBuy -= Buy;
            group.Dispose();
        });
    }

    public void SetAvailable(ShopGroup shopGroup, int levelId)
    {
        var group = GetShopVisualGroup(shopGroup);

        if(group == null)
        {
            Debug.LogError("Not found ShopVisualGroup with ShopGroup - " + shopGroup);
            return;
        }

        group.SetAvailable(levelId);
    }

    private ShopVisualGroup GetShopVisualGroup(ShopGroup shopGroup)
    {
        return shopVisualGroups.FirstOrDefault(group => group.ShopGroup == shopGroup);
    }

    #region Output

    public event Action<ShopGroup, int, int> OnBuy;

    private void Buy(ShopGroup shopGroup, int levelId, int price)
    {
        OnBuy?.Invoke(shopGroup, levelId, price);
    }

    #endregion
}

[System.Serializable]
public class ShopVisualGroup
{
    public ShopGroup ShopGroup => shopGroup;

    [SerializeField] private ShopGroup shopGroup;
    [SerializeField] private List<ShopVisual> shopVisuals = new();

    public void Initialize()
    {
        shopVisuals.ForEach(visual =>
        {
            visual.OnBuy += Buy;
            visual.Initialize();
        });
    }

    public void Dispose()
    {
        shopVisuals.ForEach(visual =>
        {
            visual.OnBuy -= Buy;
            visual.Dispose();
        });
    }

    public void SetAvailable(int id)
    {
        for (int i = 0; i < shopVisuals.Count; i++)
        {
            if(i + 1 < id)
            {
                shopVisuals[i].SetReceived();
            }
            else if(i + 1 == id)
            {
                shopVisuals[i].SetAvailabled();
            }
            else
            {
                shopVisuals[i].SetLocked();
            }
        }
    }

    #region Output

    public event Action<ShopGroup, int, int> OnBuy;

    private void Buy(int levelId, int price)
    {
        OnBuy?.Invoke(shopGroup, levelId, price);
    }

    #endregion
}
