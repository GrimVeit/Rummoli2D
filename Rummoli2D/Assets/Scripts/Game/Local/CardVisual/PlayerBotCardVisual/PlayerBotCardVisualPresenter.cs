using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBotCardVisualPresenter
{
    private readonly PlayerBotCardVisualModel _model;
    private readonly PlayerBotCardVisualView _view;

    public PlayerBotCardVisualPresenter(PlayerBotCardVisualModel model, PlayerBotCardVisualView view)
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
        _model.OnAddCard += _view.AddCard;
        _model.OnRemoveCard += _view.RemoveCard;
    }

    private void DeactivateEvents()
    {
        _model.OnAddCard -= _view.AddCard;
        _model.OnRemoveCard -= _view.RemoveCard;
    }
}
