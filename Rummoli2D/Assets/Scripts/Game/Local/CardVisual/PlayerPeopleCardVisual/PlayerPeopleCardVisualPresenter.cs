using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPeopleCardVisualPresenter : IPlayerPeopleCardVisualInteractiveActivatorProvider, IPlayerPeopleCardVisualProvider, IPlayerPeopleCardVisualEventsProvider, IPlayerPeopleSubmitEventsProvider, IPlayerPeopleSubmitActivatorProvider
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

    #region Output

    public event Action<ICard> OnChooseCard
    {
        add => _view.OnChooseCard += value;
        remove => _view.OnChooseCard -= value;
    }

    public event Action OnSubmit
    {
        add => _view.OnSubmit += value;
        remove => _view.OnSubmit -= value;
    }

    #endregion



    #region Input

    public void ActivateInteractive() => _view.ActivateInteractive();
    public void DeactivateInteractive() => _view.DeactivateInteractive();


    public void Select(ICard card) => _view.Select(card);
    public void Deselect(ICard card) => _view.Deselect(card);


    public void ActivateSubmit() => _view.ActivateSubmit();
    public void DeactivateSubmit() => _view.DeactivateSubmit();

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



public interface IPlayerPeopleSubmitActivatorProvider
{
    public void ActivateSubmit();
    public void DeactivateSubmit();
}

public interface IPlayerPeopleSubmitEventsProvider
{
    public event Action OnSubmit;
}
