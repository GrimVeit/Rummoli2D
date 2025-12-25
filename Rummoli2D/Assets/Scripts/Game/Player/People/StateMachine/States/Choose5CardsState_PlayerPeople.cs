using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choose5CardsState_PlayerPeople : IState
{
    private readonly IPlayerPeopleCardVisualInteractiveActivatorProvider _playerPeopleCardVisualInteractiveProvider;
    private readonly IPlayerPeopleCardVisualEventsProvider _playerPeopleCardVisualEventsProvider;
    private readonly IPlayerPeopleCardVisualProvider _playerPeopleCardVisualProvider;
    private readonly IPlayerPeopleSubmitEventsProvider _playerPeopleSubmitEventsProvider;
    private readonly IPlayerPeopleSubmitActivatorProvider _playerPeopleSubmitProvider;
    private readonly List<ICard> _cards = new();

    public Choose5CardsState_PlayerPeople
        (IPlayerPeopleCardVisualInteractiveActivatorProvider playerPeopleCardVisualInteractiveProvider, 
        IPlayerPeopleCardVisualEventsProvider playerPeopleCardVisualEventsProvider,
        IPlayerPeopleCardVisualProvider playerPeopleCardVisualProvider,
        IPlayerPeopleSubmitEventsProvider playerPeopleSubmitEventsProvider,
        IPlayerPeopleSubmitActivatorProvider playerPeopleSubmitProvider)
    {
        _playerPeopleCardVisualInteractiveProvider = playerPeopleCardVisualInteractiveProvider;
        _playerPeopleCardVisualEventsProvider = playerPeopleCardVisualEventsProvider;
        _playerPeopleCardVisualProvider = playerPeopleCardVisualProvider;
        _playerPeopleSubmitEventsProvider = playerPeopleSubmitEventsProvider;
        _playerPeopleSubmitProvider = playerPeopleSubmitProvider;
    }

    public void EnterState()
    {
        _playerPeopleSubmitEventsProvider.OnSubmit += Submit;
        _playerPeopleCardVisualEventsProvider.OnChooseCard += ChooseCard;

        _playerPeopleCardVisualInteractiveProvider.ActivateInteractive();
    }

    public void ExitState()
    {
        _playerPeopleSubmitEventsProvider.OnSubmit -= Submit;
        _playerPeopleCardVisualEventsProvider.OnChooseCard -= ChooseCard;

        _playerPeopleCardVisualInteractiveProvider.DeactivateInteractive();

        for (int i = 0; i < _cards.Count; i++)
        {
            _playerPeopleCardVisualProvider.Deselect(_cards[i]);
        }

        _playerPeopleSubmitProvider.DeactivateSubmit();
    }

    private void ChooseCard(ICard card)
    {
        if (_cards.Contains(card))
        {
            _cards.Remove(card);
            _playerPeopleCardVisualProvider.Deselect(card);
        }
        else
        {
            _cards.Add(card);
            _playerPeopleCardVisualProvider.Select(card);
        }

        if(_cards.Count == 5)
        {
            _playerPeopleSubmitProvider.ActivateSubmit();
        }
        else
        {
            _playerPeopleSubmitProvider.DeactivateSubmit();
        }
    }

    #region Output

    public event Action<List<ICard>> OnChooseCards;

    private void Submit()
    {
        if(_cards.Count != 5) return;

        OnChooseCards?.Invoke(_cards);
        Debug.Log("FIVE CARDS!!!");
    }

    #endregion
}
