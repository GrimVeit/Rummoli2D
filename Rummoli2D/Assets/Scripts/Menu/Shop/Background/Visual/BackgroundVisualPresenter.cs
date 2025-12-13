using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundVisualPresenter
{
    private readonly BackgroundVisualModel _model;
    private readonly BackgroundVisualView _view;

    public BackgroundVisualPresenter(BackgroundVisualModel model, BackgroundVisualView view)
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
        _model.OnSelect += _view.SetBackground;
    }

    private void DeactivateEvents()
    {
        _model.OnSelect -= _view.SetBackground;
    }
}
