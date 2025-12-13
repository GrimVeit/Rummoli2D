using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBuyVisualPresenter
{
    private readonly BackgroundBuyVisualModel _model;
    private readonly BackgroundBuyVisualView _view;

    public BackgroundBuyVisualPresenter(BackgroundBuyVisualModel model, BackgroundBuyVisualView view)
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
        _view.OnChoose += _model.ChooseBackground;
        _view.OnBuy += _model.Buy;

        _model.OnActivateBuy += _view.ActivateBuy;
        _model.OnDeactivateBuy += _view.DeactivateBuy;
        _model.OnChoose += _view.Choose;
        _model.OnUnchoose += _view.Unchoose;
        _model.OnOpen += _view.Open;
        _model.OnClose += _view.Close;
        _model.OnSelect += _view.Select;
        _model.OnDeselect += _view.Deselect;
    }

    private void DeactivateEvents()
    {
        _view.OnChoose -= _model.ChooseBackground;
        _view.OnBuy -= _model.Buy;

        _model.OnActivateBuy -= _view.ActivateBuy;
        _model.OnDeactivateBuy -= _view.DeactivateBuy;
        _model.OnChoose -= _view.Choose;
        _model.OnUnchoose -= _view.Unchoose;
        _model.OnOpen -= _view.Open;
        _model.OnClose -= _view.Close;
        _model.OnSelect -= _view.Select;
        _model.OnDeselect -= _view.Deselect;
    }
}
