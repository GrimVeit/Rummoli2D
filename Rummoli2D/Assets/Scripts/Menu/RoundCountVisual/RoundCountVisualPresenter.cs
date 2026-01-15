using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundCountVisualPresenter
{
    private readonly RoundCountVisualModel _model;
    private readonly RoundCountVisualView _view;

    public RoundCountVisualPresenter(RoundCountVisualModel model, RoundCountVisualView view)
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
        _view.OnIncrease += _model.IncreaseCount;
        _view.OnDecrease += _model.DecreaseCount;

        _model.OnRoundsCountChanged += _view.SetCount;
    }

    private void DeactivateEvents()
    {
        _view.OnIncrease -= _model.IncreaseCount;
        _view.OnDecrease -= _model.DecreaseCount;

        _model.OnRoundsCountChanged -= _view.SetCount;
    }
}
