using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRummoliVisualPresenter : ICardRummoliVisualActivator
{
    private readonly CardRummoliVisualModel _model;
    private readonly CardRummoliVisualView _view;

    public CardRummoliVisualPresenter(CardRummoliVisualModel model, CardRummoliVisualView view)
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
        _model.OnCurrentCardDataChanged += _view.SetVisual;
    }

    private void DeactivateEvents()
    {
        _model.OnCurrentCardDataChanged -= _view.SetVisual;
    }

    #region Input

    public void ActivateVisual() => _view.ActivateVisual();
    public void DeactivateVisual() => _view.DeactivateVisual();

    #endregion
}

public interface ICardRummoliVisualActivator
{
    public void ActivateVisual();
    public void DeactivateVisual();
}
