using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardDesignBuyVisualPresenter
{
    private readonly CardDesignBuyVisualModel _model;
    private readonly CardDesignBuyVisualView _view;

    public CardDesignBuyVisualPresenter(CardDesignBuyVisualModel model, CardDesignBuyVisualView view)
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
        _view.OnChoose += _model.ChooseDesign;
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
        _view.OnChoose -= _model.ChooseDesign;
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
