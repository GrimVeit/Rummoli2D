using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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

    public RummoliState_Game(IStateMachineProvider stateProvider, List<IPlayer> players, IStoreCardRummoliProvider storeCardRummoliProvider, ICardRummoliVisualActivator cardRummoliVisualActivator, IPlayerHighlightSystemProvider highlightSystemProvider, IPlayerPopupEffectSystemProvider popupEffectSystemProvider)
    {
        _stateProvider = stateProvider;
        _players = players;
        _storeCardRummoliProvider = storeCardRummoliProvider;
        _passCycle = new List<int>();
        _cardRummoliVisualActivator = cardRummoliVisualActivator;
        _highlightSystemProvider = highlightSystemProvider;
        _popupEffectSystemProvider = popupEffectSystemProvider;
    }


    public void EnterState()
    {
        Debug.Log($"[Rummoli] EnterState: {_storeCardRummoliProvider.CurrentCardData?.Suit} {_storeCardRummoliProvider.CurrentCardData?.Rank}");
        _currentPlayerIndex = 0;
        _awaitingRandomTwo = false;

        _cardRummoliVisualActivator.ActivateVisual();

        RequestCardToCurrentPlayer();
    }

    public void ExitState()
    {
        UnsubscribeCurrentPlayerEvents();
    }

    private void RequestCardToCurrentPlayer()
    {
        var player = GetPlayer(_currentPlayerIndex);
        _highlightSystemProvider.ActivateHighlight(player.Id);

        if (_storeCardRummoliProvider.CurrentCardData != null)
        {
            // Запросить обычную карту (Next)
            SubscribeNextEvents(player);
            player.ActivateRequestCard(_storeCardRummoliProvider.CurrentCardData);
        }
        else
        {
            // Текущая карта null — значит нужно ждать рандомную двойку
            _awaitingRandomTwo = true;
            SubscribeRandomTwoEvents(player);
            player.ActivateRequestRandomTwo();
        }
    }

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

    #region Event Handlers

    private void OnCardLaidNext(int playerId, ICard card)
    {
        var player = GetPlayer(playerId);
        player.DeactivateRequestCard();
        UnsubscribeNextEvents(player);
        _highlightSystemProvider.DeactivateHighlight(playerId);

        Debug.Log($"[Rummoli] Player {player.Name} laid card successfully");
        _storeCardRummoliProvider.NextCard();

        _passCycle.Clear();

        player.RemoveCard(card); // Добавляем карту в игрока
        //AdvanceTurn();

        RequestCardToCurrentPlayer();
    }

    private void OnPassNext(int playerId)
    {
        var player = GetPlayer(playerId);
        player.DeactivateRequestCard();
        UnsubscribeNextEvents(player);
        _highlightSystemProvider.DeactivateHighlight(playerId);
        _popupEffectSystemProvider.ShowPass(playerId);

        Debug.Log($"[Rummoli] Player {player.Name} passed, next card will be chosen automatically");

        _passCycle.Add(playerId);

        if(_passCycle.Count == _players.Count)
        {
            Debug.Log("[Rummoli] All players passed, next card will be chosen automatically");
            _storeCardRummoliProvider.NextCard();
            _passCycle.Clear();
        }

        AdvanceTurn();
    }

    private void OnCardLaidRandomTwo(int playerId, ICard card)
    {
        var player = GetPlayer(playerId);
        player.DeactivateRequestRandomTwo();
        UnsubscribeRandomTwoEvents(player);
        _highlightSystemProvider.DeactivateHighlight(player.Id);

        player.RemoveCard(card); // Игрок скинул случайную двойку
        _awaitingRandomTwo = false;

        // Сделать эту двойку текущей картой и продолжить Next
        _storeCardRummoliProvider.ChooseSuit(card.CardSuit); // в StoreCardRummoliPresenter она станет CurrentCardData
        _currentPlayerIndex = playerId; // Начинаем цикл с того, кто скинул двойку
        RequestCardToCurrentPlayer();
    }

    private void OnPassRandomTwo(int playerId)
    {
        var player = GetPlayer(playerId);
        player.DeactivateRequestRandomTwo();
        UnsubscribeRandomTwoEvents(player);
        _highlightSystemProvider.DeactivateHighlight(playerId);
        _popupEffectSystemProvider.ShowPass(playerId);

        _passCycle.Add(playerId);

        if (_passCycle.Count == _players.Count)
        {
            Debug.Log("[Rummoli] All players passed, next card will be chosen automatically");
            _storeCardRummoliProvider.ChooseRandomSuit();
            _passCycle.Clear();
        }

        AdvanceTurnRandomTwo();
    }

    #endregion

    #region Turn Management

    private void AdvanceTurn()
    {
        // Следующий игрок по кругу
        _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;

        // Запросить карту следующему игроку
        RequestCardToCurrentPlayer();
    }

    private void AdvanceTurnRandomTwo()
    {
        _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;

        // Если все прошли и никто не выложил двойку, оставляем CurrentCardData null, чтобы состояние обработало выбор рандомной двойки
        if (_currentPlayerIndex == 0)
        {
            Debug.Log("[Rummoli] No player had a two, next card will be chosen randomly");
            _storeCardRummoliProvider.CurrentCardData = null;
        }

        RequestCardToCurrentPlayer();
    }

    private IPlayer GetPlayer(int id)
    {
        return _players.Find(data => data.Id == id);
    }

    #endregion
}
