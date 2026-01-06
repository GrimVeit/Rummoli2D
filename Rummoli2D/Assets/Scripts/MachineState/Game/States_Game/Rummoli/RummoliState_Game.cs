using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RummoliState_Game : IState
{
    private readonly IStateMachineProvider _stateProvider;
    private readonly List<IPlayer> _players;
    private readonly IStoreCardRummoliProvider _storeCardRummoliProvider;
    private readonly ICardRummoliVisualActivator _cardRummoliVisualActivator;
    private readonly IPlayerHighlightSystemProvider _highlightSystemProvider;
    private readonly IPlayerPopupEffectSystemProvider _popupEffectSystemProvider;
    private readonly ISectorConditionCheckerProvider _sectorConditionCheckerProvider;
    private readonly IPlayerPresentationSystemProvider _playerPresentationSystemProvider;
    private readonly IBetSystemProvider _betSystemProvider;
    private readonly IBetSystemEventsProvider _betSystemEventsProvider;
    private readonly ICounterPassPlayerSystemProvider _counterPassPlayerSystemProvider;
    private readonly ICounterPassPlayerSystemActivatorProvider _counterPassPlayerSystemActivatorProvider;
    private readonly IRummoliTablePresentationSystemProvider _rummoliTablePresentationSystemProvider;

    private int _currentPlayerIndex = 0;
    private bool _awaitingRandomTwo = false;
    private bool _isGameEnding = false;

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
        IPlayerPopupEffectSystemProvider popupEffectSystemProvider,
        ISectorConditionCheckerProvider sectorConditionCheckerProvider,
        IPlayerPresentationSystemProvider playerPresentationSystemProvider,
        IBetSystemProvider betSystemProvider,
        IBetSystemEventsProvider betSystemEventsProvider,
        ICounterPassPlayerSystemProvider counterPassPlayerSystemProvider,
        ICounterPassPlayerSystemActivatorProvider counterPassPlayerSystemActivatorProvider,
        IRummoliTablePresentationSystemProvider rummoliTablePresentationSystemProvider)
    {
        _stateProvider = stateProvider;
        _players = players;
        _storeCardRummoliProvider = storeCardRummoliProvider;
        _cardRummoliVisualActivator = cardRummoliVisualActivator;
        _highlightSystemProvider = highlightSystemProvider;
        _popupEffectSystemProvider = popupEffectSystemProvider;
        _sectorConditionCheckerProvider = sectorConditionCheckerProvider;
        _playerPresentationSystemProvider = playerPresentationSystemProvider;
        _betSystemProvider = betSystemProvider;
        _betSystemEventsProvider = betSystemEventsProvider;
        _counterPassPlayerSystemProvider = counterPassPlayerSystemProvider;
        _counterPassPlayerSystemActivatorProvider = counterPassPlayerSystemActivatorProvider;
        _rummoliTablePresentationSystemProvider = rummoliTablePresentationSystemProvider;
    }

    #region State

    public void EnterState()
    {
        _betSystemEventsProvider.OnReturnBet += ReturnBet;

        _currentPlayerIndex = 0;
        _awaitingRandomTwo = false;
        _isGameEnding = false;
        _counterPassPlayerSystemProvider.SetTotalPlayers(_players.Count);
        _counterPassPlayerSystemProvider.Reset();

        _cardRummoliVisualActivator.ActivateVisual();
        Coroutines.Start(RequestCardRoutine());
    }

    public void ExitState()
    {
        _betSystemEventsProvider.OnReturnBet -= ReturnBet;

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
            _cardRummoliVisualActivator.ActivateVisual();
            SubscribeNextEvents(player);
            player.ActivateRequestCard(_storeCardRummoliProvider.CurrentCardData);
        }
        else
        {
            _cardRummoliVisualActivator.DeactivateVisual();
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

        _sectorConditionCheckerProvider.AddCard(playerId, card);

        _cardRummoliVisualActivator.DeactivateVisual();

        yield return new WaitForSeconds(CARD_LAID_DELAY);

        _storeCardRummoliProvider.NextCard();
        _counterPassPlayerSystemActivatorProvider.DeactivateVisual(() => _counterPassPlayerSystemProvider.Reset());

        //

        yield return HandleClosedSectorsRoutine();

        yield return HandleRummoliWinRoutine(playerId);

        //

        if (IsGameOver())
        {
            ChangeStateToOther();
            yield break;
        }

        yield return RequestCardRoutine();
    }

    private IEnumerator PassNextRoutine(int playerId)
    {
        var player = GetPlayer(playerId);

        player.DeactivateRequestCard();
        UnsubscribeNextEvents(player);
        _highlightSystemProvider.DeactivateHighlight(playerId);
        _popupEffectSystemProvider.ShowPass(playerId);

        _counterPassPlayerSystemProvider.AddPass();
        _counterPassPlayerSystemActivatorProvider.ActivateVisual();

        yield return new WaitForSeconds(PASS_DELAY);

        if(_counterPassPlayerSystemProvider.AllPassed)
        {
            _storeCardRummoliProvider.NextCard();
            _counterPassPlayerSystemProvider.Reset();
        }

        if (IsGameOver())
        {
            ChangeStateToOther();
            yield break;
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

        _sectorConditionCheckerProvider.AddCard(playerId, card);

        _cardRummoliVisualActivator.DeactivateVisual();

        yield return new WaitForSeconds(CARD_LAID_DELAY);

        _awaitingRandomTwo = false;
        _counterPassPlayerSystemActivatorProvider.DeactivateVisual(() => _counterPassPlayerSystemProvider.Reset());
        _storeCardRummoliProvider.ChooseSuit(card.CardSuit);
        _currentPlayerIndex = playerId;

        //

        yield return HandleClosedSectorsRoutine();

        yield return HandleRummoliWinRoutine(playerId);

        //

        if (IsGameOver())
        {
            ChangeStateToOther();
            yield break;
        }

        yield return RequestCardRoutine();
    }

    private IEnumerator PassRandomTwoRoutine(int playerId)
    {
        var player = GetPlayer(playerId);

        player.DeactivateRequestRandomTwo();
        UnsubscribeRandomTwoEvents(player);
        _highlightSystemProvider.DeactivateHighlight(playerId);
        _popupEffectSystemProvider.ShowPass(playerId);

        _counterPassPlayerSystemProvider.AddPass();
        _counterPassPlayerSystemActivatorProvider.ActivateVisual();

        yield return new WaitForSeconds(PASS_DELAY);

        if(_counterPassPlayerSystemProvider.AllPassed)
        {
            _storeCardRummoliProvider.ChooseRandomSuit();
            _counterPassPlayerSystemProvider.Reset();
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

    #region SectorReward

    private IEnumerator HandleClosedSectorsRoutine()
    {
        if (!_sectorConditionCheckerProvider.IsHaveClosedSectors())
            yield break;

        var closedSectors = _sectorConditionCheckerProvider.GetClosedSectors();

        foreach (var (sectorIndex, playerId) in closedSectors)
        {
            yield return HandleClosedSectorRoutine(
                sectorIndex,
                playerId
            );

            _sectorConditionCheckerProvider.ClaimSector(sectorIndex);
        }
    }

    private IEnumerator HandleClosedSectorRoutine(int sectorIndex, int playerId)
    {
        yield return new WaitForSeconds(0.3f);

        int startIndex = UnityEngine.Random.Range(0, _players.Count);

        for (int i = 0; i < _players.Count; i++)
        {
            int index = (startIndex + i) % _players.Count;


            if (_players[index].Id == playerId)
            {
                if (_players[index].Id == 0)
                {
                    _playerPresentationSystemProvider.HideCards(0);

                    yield return new WaitForSeconds(0.2f);

                    _playerPresentationSystemProvider.MoveToLayout(0, "Table");

                    yield return new WaitForSeconds(0.3f);
                }
            }
            else
            {
                _playerPresentationSystemProvider.Hide(_players[index].Id);
            }

            yield return new WaitForSeconds(0.1f);
        }

        _cardRummoliVisualActivator.DeactivateVisual();

        yield return new WaitForSeconds(0.2f);

        _rummoliTablePresentationSystemProvider.MoveToLayout("Center");

        yield return new WaitForSeconds(0.5f);

        _betSystemProvider.StartReturnBet(playerId, sectorIndex);

        yield return new WaitForSeconds(1);

        startIndex = UnityEngine.Random.Range(0, _players.Count);

        for (int i = 0; i < _players.Count; i++)
        {
            int index = (startIndex + i) % _players.Count;

            if (_players[index].Id == playerId)
            {
                if (_players[index].Id == 0)
                {
                    _playerPresentationSystemProvider.MoveToLayout(0, "Game");

                    yield return new WaitForSeconds(0.3f);

                    _playerPresentationSystemProvider.ShowCards(0);
                }
            }
            else
            {
                _playerPresentationSystemProvider.Show(_players[index].Id);
            }

            yield return new WaitForSeconds(0.2f);
        }

        _rummoliTablePresentationSystemProvider.MoveToLayout("Small");

        yield return new WaitForSeconds(0.2f);

        _cardRummoliVisualActivator.ActivateVisual();

        yield return new WaitForSeconds(0.2f);
    }

    private IEnumerator HandleRummoliWinRoutine(int playerId)
    {
        if(IsPlayerOutOfCards(GetPlayer(playerId)) == false) yield break;

        if (_isGameEnding) yield break;

        _isGameEnding = true;

        const int RUMMOLI_SECTOR_INDEX = 8;

        yield return HandleClosedSectorRoutine(
            RUMMOLI_SECTOR_INDEX,
            playerId
        );
    }

    private void ReturnBet(int playerId, int score)
    {
        var player = GetPlayer(playerId);

        player.AddScore(score);
    }

    #endregion

    #region Other

    private bool IsPlayerOutOfCards(IPlayer player)
    {
        return player.CardCount == 0;
    }

    private bool IsGameOver()
    {
        return _storeCardRummoliProvider.IsFinished;
    }

    #endregion

    private void ChangeStateToOther()
    {
        _stateProvider.EnterState(_stateProvider.GetState<RoundCompleteState_Game>());
    }
}

