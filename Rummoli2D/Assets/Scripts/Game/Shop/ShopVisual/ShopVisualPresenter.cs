using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopVisualPresenter
{
    private readonly ShopVisualModel _model;
    private readonly ShopVisualView _view;

    public ShopVisualPresenter(ShopVisualModel model, ShopVisualView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnBuy += _model.Buy;

        _model.OnLevelShopChanged += _view.SetAvailable;
    }

    private void DeactivateEvents()
    {
        _view.OnBuy -= _model.Buy;

        _model.OnLevelShopChanged -= _view.SetAvailable;
    }
}
