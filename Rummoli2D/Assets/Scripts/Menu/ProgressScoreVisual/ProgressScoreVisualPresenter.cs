using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressScoreVisualPresenter
{
    private readonly ProgressScoreVisualModel _model;
    private readonly ProgressScoreVisualView _view;

    public ProgressScoreVisualPresenter(ProgressScoreVisualModel model, ProgressScoreVisualView view)
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
        _model.OnScoreProgressChanged += _view.SetScoreProgress;
    }

    private void DeactivateEvents()
    {
        _model.OnScoreProgressChanged -= _view.SetScoreProgress;
    }
}
