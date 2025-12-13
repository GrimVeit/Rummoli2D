using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScrollPresenter : IShopScrollProvider
{
    private readonly ShopScrollModel _model;
    private readonly ShopScrollView _view;

    public ShopScrollPresenter(ShopScrollModel model, ShopScrollView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _view.Dispose();
    }

    private void ActivateEvents()
    {

    }

    private void DeactivateEvents()
    {

    }

    #region Input

    public void ResetPage() => _view.ResetPage();
    public void CloseAllPage() => _view.CloseAllPages();

    #endregion
}

public interface IShopScrollProvider
{
    void ResetPage();
    void CloseAllPage();
}
