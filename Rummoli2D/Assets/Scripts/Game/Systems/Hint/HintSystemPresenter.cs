using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintSystemPresenter : IHintSystemProvider, IHintSystemActivatorProvider
{
    private readonly HintSystemModel _model;
    private readonly HintSystemView _view;

    public HintSystemPresenter(HintSystemModel model, HintSystemView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        _model.OnShow += _view.Show;
        _model.OnHide += _view.Hide;
        _model.OnShowAll += _view.ShowAll;
        _model.OnHideAll += _view.HideAll;
        _model.OnDeleteAll += _view.DeleteAll;
    }

    private void DeactivateEvents()
    {
        _model.OnShow -= _view.Show;
        _model.OnHide -= _view.Hide;
        _model.OnShowAll -= _view.ShowAll;
        _model.OnHideAll -= _view.HideAll;
        _model.OnDeleteAll -= _view.DeleteAll;
    }

    #region Input

    public void Show(string key) => _model.Show(key);
    public void Hide(string key) => _model.Hide(key);
    public void DeleteAll() => _model.DeleteAll();


    public void ShowAll() => _model.ShowAll();
    public void HideAll() => _model.HideAll();

    #endregion
}

public interface IHintSystemProvider
{
    public void Show(string key);
    public void Hide(string key);
    public void DeleteAll();
}

public interface IHintSystemActivatorProvider
{
    public void ShowAll();
    public void HideAll();
}
