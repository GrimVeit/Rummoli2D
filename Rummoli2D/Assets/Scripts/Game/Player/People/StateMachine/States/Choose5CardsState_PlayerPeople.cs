using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choose5CardsState_PlayerPeople : IState
{
    private readonly IPlayerPeopleCardVisualInteractiveActivatorProvider _playerPeopleCardVisualInteractiveProvider;
    private readonly IPlayerPeopleCardVisualEventsProvider _playerPeopleCardVisualEventsProvider;
    private readonly IPlayerPeopleCardVisualProvider _playerPeopleCardVisualProvider;
    private readonly IPlayerPeopleInputEventsProvider _playerPeopleSubmitEventsProvider;
    private readonly IPlayerPeopleInputActivatorProvider _playerPeopleSubmitProvider;
    private readonly ICardPokerSelectorPlayerProvider _cardPokerSelectorPlayerProvider;
    private readonly List<ICard> _cards = new();

    public Choose5CardsState_PlayerPeople
        (IPlayerPeopleCardVisualInteractiveActivatorProvider playerPeopleCardVisualInteractiveProvider, 
        IPlayerPeopleCardVisualEventsProvider playerPeopleCardVisualEventsProvider,
        IPlayerPeopleCardVisualProvider playerPeopleCardVisualProvider,
        IPlayerPeopleInputEventsProvider playerPeopleSubmitEventsProvider,
        IPlayerPeopleInputActivatorProvider playerPeopleSubmitProvider,
        ICardPokerSelectorPlayerProvider cardPokerSelectorPlayerProvider)
    {
        _playerPeopleCardVisualInteractiveProvider = playerPeopleCardVisualInteractiveProvider;
        _playerPeopleCardVisualEventsProvider = playerPeopleCardVisualEventsProvider;
        _playerPeopleCardVisualProvider = playerPeopleCardVisualProvider;
        _playerPeopleSubmitEventsProvider = playerPeopleSubmitEventsProvider;
        _playerPeopleSubmitProvider = playerPeopleSubmitProvider;
        _cardPokerSelectorPlayerProvider = cardPokerSelectorPlayerProvider;
    }

    public void EnterState()
    {
        _playerPeopleSubmitEventsProvider.OnChoose += Submit;
        _playerPeopleCardVisualEventsProvider.OnChooseCard += ChooseCard;

        _playerPeopleCardVisualInteractiveProvider.ActivateInteractive();
    }

    public void ExitState()
    {
        _playerPeopleSubmitEventsProvider.OnChoose -= Submit;
        _playerPeopleCardVisualEventsProvider.OnChooseCard -= ChooseCard;

        _playerPeopleCardVisualInteractiveProvider.DeactivateInteractive();

        for (int i = 0; i < _cards.Count; i++)
        {
            _playerPeopleCardVisualProvider.Deselect(_cards[i]);
        }

        _playerPeopleSubmitProvider.DeactivateChoose();
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
            _playerPeopleSubmitProvider.ActivateChoose();
        }
        else
        {
            _playerPeopleSubmitProvider.DeactivateChoose();
        }
    }

    #region Output

    public event Action<List<ICard>> OnChooseCards;

    private void Submit()
    {
        if(_cards.Count != 5) return;

        OnChooseCards?.Invoke(new List<ICard>(_cards));
        Debug.Log("FIVE CARDS!!!");
    }

    #endregion
}
