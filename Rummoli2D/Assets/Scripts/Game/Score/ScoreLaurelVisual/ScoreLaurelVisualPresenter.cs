using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLaurelVisualPresenter
{
    private readonly ScoreLaurelVisualModel _model;
    private readonly ScoreLaurelVisualView _view;

    public ScoreLaurelVisualPresenter(ScoreLaurelVisualModel model, ScoreLaurelVisualView view)
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
        _model.OnScoreLaurelChanged += _view.SetLaurel;
    }

    private void DeactivateEvents()
    {
        _model.OnScoreLaurelChanged -= _view.SetLaurel;
    }
}
