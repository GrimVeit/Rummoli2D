using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundNumberVisualPresenter
{
    private readonly RoundNumberVisualModel _model;
    private readonly RoundNumberVisualView _view;

    public RoundNumberVisualPresenter(RoundNumberVisualModel model, RoundNumberVisualView view)
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
        _model.OnChangeRoundName += _view.SetName;
    }

    private void DeactivateEvents()
    {
        _model.OnChangeRoundName -= _view.SetName;
    }
}
