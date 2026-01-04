using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RummoliState_Game : IState
{
    private readonly IStateMachineProvider _stateProvider;
    private readonly List<IPlayer> _players;
    private readonly IStoreCardRummoliProvider _storeCardRummoliProvider;
    private readonly List<int> _passCycle;
    private readonly ICardRummoliVisualActivator _cardRummoliVisualActivator;
    private readonly IPlayerHighlightSystemProvider _highlightSystemProvider;
    private readonly IPlayerPopupEffectSystemProvider _popupEffectSystemProvider;

    private int _currentPlayerIndex = 0;
    private bool _awaitingRandomTwo = false;

    // 🔹 Тайминги (можно вынести в конфиг)
    private const float TURN_DELAY = 0.4f;
    private const float PASS_DELAY = 0.6f;
    private const float CARD_LAID_DELAY = 0.5f;

    public RummoliState_Game(
        IStateMachineProvider stateProvider,
        List<IPlayer> players,
        IStoreCardRummoliProvider storeCardRummoliProvider,
        ICardRummoliVisualActivator cardRummoliVisualActivator,
        IPlayerHighlightSystemProvider highlightSystemProvider,
        IPlayerPopupEffectSystemProvider popupEffectSystemProvider)
    {
        _stateProvider = stateProvider;
        _players = players;
        _storeCardRummoliProvider = storeCardRummoliProvider;
        _passCycle = new List<int>();
        _cardRummoliVisualActivator = cardRummoliVisualActivator;
        _highlightSystemProvider = highlightSystemProvider;
        _popupEffectSystemProvider = popupEffectSystemProvider;
    }

    #region State

    public void EnterState()
    {
        _currentPlayerIndex = 0;
        _awaitingRandomTwo = false;
        _passCycle.Clear();

        _cardRummoliVisualActivator.ActivateVisual();
        Coroutines.Start(RequestCardRoutine());
    }

    public void ExitState()
    {
        _cardRummoliVisualActivator.DeactivateVisual();

        UnsubscribeCurrentPlayerEvents();
    }

    #endregion

    #region Request

    private IEnumerator RequestCardRoutine()
    {
        yield return new WaitForSeconds(TURN_DELAY);

        var player = GetPlayer(_currentPlayerIndex);
        _highlightSystemProvider.ActivateHighlight(player.Id);

        if (_storeCardRummoliProvider.CurrentCardData != null)
        {
            SubscribeNextEvents(player);
            player.ActivateRequestCard(_storeCardRummoliProvider.CurrentCardData);
        }
        else
        {
            _awaitingRandomTwo = true;
            SubscribeRandomTwoEvents(player);
            player.ActivateRequestRandomTwo();
        }
    }

    #endregion

    #region Event Subscriptions

    private void SubscribeNextEvents(IPlayer player)
    {
        player.OnCardLaid_Next += OnCardLaidNext;
        player.OnPass_Next += OnPassNext;
    }

    private void UnsubscribeNextEvents(IPlayer player)
    {
        player.OnCardLaid_Next -= OnCardLaidNext;
        player.OnPass_Next -= OnPassNext;
    }

    private void SubscribeRandomTwoEvents(IPlayer player)
    {
        player.OnCardLaid_RandomTwo += OnCardLaidRandomTwo;
        player.OnPass_RandomTwo += OnPassRandomTwo;
    }

    private void UnsubscribeRandomTwoEvents(IPlayer player)
    {
        player.OnCardLaid_RandomTwo -= OnCardLaidRandomTwo;
        player.OnPass_RandomTwo -= OnPassRandomTwo;
    }

    private void UnsubscribeCurrentPlayerEvents()
    {
        var player = GetPlayer(_currentPlayerIndex);
        if (_awaitingRandomTwo)
            UnsubscribeRandomTwoEvents(player);
        else
            UnsubscribeNextEvents(player);
    }

    #endregion

    #region Events → Coroutines

    private void OnCardLaidNext(int playerId, ICard card)
    {
        Coroutines.Start(CardLaidNextRoutine(playerId, card));
    }

    private void OnPassNext(int playerId)
    {
        Coroutines.Start(PassNextRoutine(playerId));
    }

    private void OnCardLaidRandomTwo(int playerId, ICard card)
    {
        Coroutines.Start(CardLaidRandomTwoRoutine(playerId, card));
    }

    private void OnPassRandomTwo(int playerId)
    {
        Coroutines.Start(PassRandomTwoRoutine(playerId));
    }

    #endregion

    #region Routines

    private IEnumerator CardLaidNextRoutine(int playerId, ICard card)
    {
        var player = GetPlayer(playerId);

        player.DeactivateRequestCard();
        UnsubscribeNextEvents(player);
        _highlightSystemProvider.DeactivateHighlight(playerId);

        player.RemoveCard(card);

        yield return new WaitForSeconds(CARD_LAID_DELAY);

        _storeCardRummoliProvider.NextCard();
        _passCycle.Clear();

        yield return RequestCardRoutine();
    }

    private IEnumerator PassNextRoutine(int playerId)
    {
        var player = GetPlayer(playerId);

        player.DeactivateRequestCard();
        UnsubscribeNextEvents(player);
        _highlightSystemProvider.DeactivateHighlight(playerId);
        _popupEffectSystemProvider.ShowPass(playerId);

        _passCycle.Add(playerId);

        yield return new WaitForSeconds(PASS_DELAY);

        if (_passCycle.Count == _players.Count)
        {
            _storeCardRummoliProvider.NextCard();
            _passCycle.Clear();
        }

        AdvanceTurn();
        yield return RequestCardRoutine();
    }

    private IEnumerator CardLaidRandomTwoRoutine(int playerId, ICard card)
    {
        var player = GetPlayer(playerId);

        player.DeactivateRequestRandomTwo();
        UnsubscribeRandomTwoEvents(player);
        _highlightSystemProvider.DeactivateHighlight(playerId);

        player.RemoveCard(card);

        yield return new WaitForSeconds(CARD_LAID_DELAY);

        _awaitingRandomTwo = false;
        _passCycle.Clear();
        _storeCardRummoliProvider.ChooseSuit(card.CardSuit);
        _currentPlayerIndex = playerId;

        yield return RequestCardRoutine();
    }

    private IEnumerator PassRandomTwoRoutine(int playerId)
    {
        var player = GetPlayer(playerId);

        player.DeactivateRequestRandomTwo();
        UnsubscribeRandomTwoEvents(player);
        _highlightSystemProvider.DeactivateHighlight(playerId);
        _popupEffectSystemProvider.ShowPass(playerId);

        _passCycle.Add(playerId);

        yield return new WaitForSeconds(PASS_DELAY);

        if (_passCycle.Count == _players.Count)
        {
            _storeCardRummoliProvider.ChooseRandomSuit();
            _passCycle.Clear();
        }

        AdvanceTurn();
        yield return RequestCardRoutine();
    }

    #endregion

    #region Turn

    private void AdvanceTurn()
    {
        _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
    }

    private IPlayer GetPlayer(int id)
    {
        return _players.Find(p => p.Id == id);
    }

    #endregion
}

