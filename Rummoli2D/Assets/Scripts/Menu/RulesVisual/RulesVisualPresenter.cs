using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesVisualPresenter : IRulesVisualProvider
{
    private readonly RulesVisualModel _model;
    private readonly RulesVisualView _view;

    public RulesVisualPresenter(RulesVisualModel model, RulesVisualView view)
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
        _view.OnClickLeftRight += _model.ClickLeftRight;
    }

    private void DeactivateEvents()
    {
        _view.OnClickLeftRight -= _model.ClickLeftRight;
    }

    #region Input

    public void ResetPage() => _view.ResetPage();

    #endregion
}

public interface IRulesVisualProvider
{
    void ResetPage();
}
