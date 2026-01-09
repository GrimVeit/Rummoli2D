using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choose5CardsState_PlayerPeople : IState
{
    private readonly IPlayerPeopleCardVisualInteractiveActivatorProvider _playerPeopleCardVisualInteractiveProvider;
    private readonly IPlayerPeopleCardVisualEventsProvider _playerPeopleCardVisualEventsProvider;
    private readonly IPlayerPeopleCardVisualProvider _playerPeopleCardVisualProvider;
    private readonly IPlayerPeopleInputEventsProvider _playerPeopleInputEventsProvider;
    private readonly IPlayerPeopleInputActivatorProvider _playerPeopleInputProvider;
    private readonly ICardPokerSelectorPlayerProvider _cardPokerSelectorPlayerProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly List<ICard> _cards = new();

    public Choose5CardsState_PlayerPeople
        (IPlayerPeopleCardVisualInteractiveActivatorProvider playerPeopleCardVisualInteractiveProvider, 
        IPlayerPeopleCardVisualEventsProvider playerPeopleCardVisualEventsProvider,
        IPlayerPeopleCardVisualProvider playerPeopleCardVisualProvider,
        IPlayerPeopleInputEventsProvider playerPeopleSubmitEventsProvider,
        IPlayerPeopleInputActivatorProvider playerPeopleSubmitProvider,
        ICardPokerSelectorPlayerProvider cardPokerSelectorPlayerProvider,
        UIGameRoot sceneRoot)
    {
        _playerPeopleCardVisualInteractiveProvider = playerPeopleCardVisualInteractiveProvider;
        _playerPeopleCardVisualEventsProvider = playerPeopleCardVisualEventsProvider;
        _playerPeopleCardVisualProvider = playerPeopleCardVisualProvider;
        _playerPeopleInputEventsProvider = playerPeopleSubmitEventsProvider;
        _playerPeopleInputProvider = playerPeopleSubmitProvider;
        _cardPokerSelectorPlayerProvider = cardPokerSelectorPlayerProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _playerPeopleInputEventsProvider.OnChoose += Submit;
        _playerPeopleCardVisualEventsProvider.OnChooseCard += ChooseCard;

        _playerPeopleInputProvider.SetMainChoose();
        _playerPeopleCardVisualInteractiveProvider.ActivateInteractive();
        _sceneRoot.OpenRightPanel();
    }

    public void ExitState()
    {
        _playerPeopleInputEventsProvider.OnChoose -= Submit;
        _playerPeopleCardVisualEventsProvider.OnChooseCard -= ChooseCard;

        _playerPeopleCardVisualInteractiveProvider.DeactivateInteractive();

        for (int i = 0; i < _cards.Count; i++)
        {
            _playerPeopleCardVisualProvider.Deselect(_cards[i]);
        }

        _cards.Clear();

        _playerPeopleInputProvider.DeactivateChoose();
        _sceneRoot.CloseRightPanel();
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
            _playerPeopleInputProvider.ActivateChoose();
        }
        else
        {
            _playerPeopleInputProvider.DeactivateChoose();
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
