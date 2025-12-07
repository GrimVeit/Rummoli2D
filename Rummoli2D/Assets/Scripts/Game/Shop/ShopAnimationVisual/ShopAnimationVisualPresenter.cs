using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAnimationVisualPresenter
{
    private readonly ShopAnimationVisualModel _model;
    private readonly ShopAnimationVisualView _view;

    public ShopAnimationVisualPresenter(ShopAnimationVisualModel model, ShopAnimationVisualView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnLevelShopChanged += _view.SetLevelShopChanged;
    }

    private void DeactivateEvents()
    {
        _model.OnLevelShopChanged -= _view.SetLevelShopChanged;
    }
}
