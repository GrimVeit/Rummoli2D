using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPeopleCardVisualPresenter
{
    private readonly PlayerPeopleCardVisualModel _model;
    private readonly PlayerPeopleCardVisualView _view;

    public PlayerPeopleCardVisualPresenter(PlayerPeopleCardVisualModel model, PlayerPeopleCardVisualView view)
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
        _model.OnAddCard += _view.AddCard;
        _model.OnRemoveCard += _view.RemoveCard;
    }

    private void DeactivateEvents()
    {
        _model.OnAddCard -= _view.AddCard;
        _model.OnRemoveCard -= _view.RemoveCard;
    }
}
