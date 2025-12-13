using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipBuyPresenter
{
    private readonly ChipBuyModel _model;
    private readonly ChipBuyView _view;

    public ChipBuyPresenter(ChipBuyModel model, ChipBuyView view)
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
        _view.OnIncrease += _model.AddChip;
        _view.OnDecrease += _model.RemoveChip;
    }

    private void DeactivateEvents()
    {
        _view.OnIncrease -= _model.AddChip;
        _view.OnDecrease -= _model.RemoveChip;
    }
}
