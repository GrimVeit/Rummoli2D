using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRequestCard_PlayerPeople : IState
{
    private readonly IPlayerPeopleCardVisualInteractiveActivatorProvider _playerPeopleCardVisualInteractiveProvider;
    private readonly IPlayerPeopleCardVisualEventsProvider _playerPeopleCardVisualEventsProvider;
    private readonly IPlayerPeopleCardVisualProvider _playerPeopleCardVisualProvider;
    private readonly IPlayerPeopleInputEventsProvider _playerPeopleInputEventsProvider;
    private readonly IPlayerPeopleInputActivatorProvider _playerPeopleInputProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IHintSystemProvider _hintSystemProvider;

    private ICard _currentChooseCard = null;
    private CardData _currentCardData;

    public ChooseRequestCard_PlayerPeople
        (IPlayerPeopleCardVisualInteractiveActivatorProvider playerPeopleCardVisualInteractiveProvider,
        IPlayerPeopleCardVisualEventsProvider playerPeopleCardVisualEventsProvider,
        IPlayerPeopleCardVisualProvider playerPeopleCardVisualProvider,
        IPlayerPeopleInputEventsProvider playerPeopleSubmitEventsProvider,
        IPlayerPeopleInputActivatorProvider playerPeopleSubmitProvider,
        UIGameRoot sceneRoot,
        IHintSystemProvider hintSystemProvider)
    {
        _playerPeopleCardVisualInteractiveProvider = playerPeopleCardVisualInteractiveProvider;
        _playerPeopleCardVisualEventsProvider = playerPeopleCardVisualEventsProvider;
        _playerPeopleCardVisualProvider = playerPeopleCardVisualProvider;
        _playerPeopleInputEventsProvider = playerPeopleSubmitEventsProvider;
        _playerPeopleInputProvider = playerPeopleSubmitProvider;
        _sceneRoot = sceneRoot;
        _hintSystemProvider = hintSystemProvider;
    }

    public void EnterState()
    {
        _playerPeopleInputEventsProvider.OnChoose += Choose;
        _playerPeopleInputEventsProvider.OnPass += Pass;
        _playerPeopleCardVisualEventsProvider.OnChooseCard += ChooseCard;

        _playerPeopleInputProvider.SetMainPass();
        _playerPeopleCardVisualInteractiveProvider.ActivateInteractive();
        _playerPeopleInputProvider.ActivatePass();

        _sceneRoot.OpenRightPanel();
        _hintSystemProvider.Show("ChooseNextCard");
    }

    public void ExitState()
    {
        _playerPeopleInputEventsProvider.OnChoose -= Choose;
        _playerPeopleInputEventsProvider.OnPass -= Pass;
        _playerPeopleCardVisualEventsProvider.OnChooseCard -= ChooseCard;

        _playerPeopleCardVisualInteractiveProvider.DeactivateInteractive();

        if (_currentChooseCard != null)
            _playerPeopleCardVisualProvider.Deselect(_currentChooseCard);

        _playerPeopleInputProvider.DeactivateChoose();
        _playerPeopleInputProvider.DeactivatePass();

        _sceneRoot.CloseRightPanel();
        _hintSystemProvider.Hide("ChooseNextCard");
    }

    public void SetCardData(CardData cardData)
    {
        _currentCardData = cardData;
    }

    private void ChooseCard(ICard card)
    {
        if(_currentCardData == null)
        {
            Debug.LogError("NOT FOUND CARDDATA");
            return;
        }

        if(_currentChooseCard == card)
        {
            _playerPeopleCardVisualProvider.Deselect(_currentChooseCard);
            _currentChooseCard = null;
            _playerPeopleInputProvider.DeactivateChoose();
            return;
        }

        if (_currentCardData.Suit == card.CardSuit && _currentCardData.Rank == card.CardRank)
        {
            _currentChooseCard = card;
            _playerPeopleCardVisualProvider.Select(_currentChooseCard);
            _playerPeopleInputProvider.ActivateChoose();
        }
    }

    #region Output

    public event Action<ICard> OnCardLaid;
    public event Action OnPass;

    private void Choose()
    {
        OnCardLaid?.Invoke(_currentChooseCard);
        Debug.Log("CHOOSE CARD!!!");
    }

    private void Pass()
    {
        OnPass?.Invoke();
        Debug.Log("PASS!!!");
    }

    #endregion
}
