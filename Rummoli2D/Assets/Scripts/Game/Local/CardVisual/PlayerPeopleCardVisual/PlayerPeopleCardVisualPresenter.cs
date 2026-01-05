using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPeopleCardVisualPresenter : IPlayerPeopleCardVisualInteractiveActivatorProvider, IPlayerPeopleCardVisualProvider, IPlayerPeopleCardVisualEventsProvider
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
        _model.OnDeleteCards += _view.DeleteCards;
    }

    private void DeactivateEvents()
    {
        _model.OnAddCard -= _view.AddCard;
        _model.OnRemoveCard -= _view.RemoveCard;
        _model.OnDeleteCards -= _view.DeleteCards;
    }

    #region Output

    public event Action<ICard> OnChooseCard
    {
        add => _view.OnChooseCard += value;
        remove => _view.OnChooseCard -= value;
    }

    #endregion



    #region Input

    public void ActivateInteractive() => _view.ActivateInteractive();
    public void DeactivateInteractive() => _view.DeactivateInteractive();


    public void Select(ICard card) => _view.Select(card);
    public void Deselect(ICard card) => _view.Deselect(card);


    #endregion
}

public interface IPlayerPeopleCardVisualEventsProvider
{
    public event Action<ICard> OnChooseCard;
}

public interface IPlayerPeopleCardVisualInteractiveActivatorProvider
{
    public void ActivateInteractive();
    public void DeactivateInteractive();
}

public interface IPlayerPeopleCardVisualProvider
{
    public void Select(ICard card);
    public void Deselect(ICard card);
}
