using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealCardsState_Game : IState
{
    private readonly IStateMachineProvider _stateMachineProvider;
    private readonly List<IPlayer> _players;
    private readonly ICardSpawnerSystemProvider _cardSpawnerProvider;
    private readonly ICardSpawnerSystemEventsProvider _cardSpawnerEventsProvider;
    private readonly ICardBankPresentationSystemProvider _cardBankPresentationSystemProvider;
    private readonly int _cardsPerPlayer;
    private int _cardsDealt = 0;

    private IEnumerator timer;

    public DealCardsState_Game(IStateMachineProvider stateMachineProvider, List<IPlayer> players, ICardSpawnerSystemProvider cardSpawnerProvider, ICardSpawnerSystemEventsProvider cardSpawnerEventsProvider, ICardBankPresentationSystemProvider cardBankPresentationSystemProvider)
    {
        _stateMachineProvider = stateMachineProvider;
        _players = players;
        _cardSpawnerProvider = cardSpawnerProvider;
        _cardSpawnerEventsProvider = cardSpawnerEventsProvider;
        _cardBankPresentationSystemProvider = cardBankPresentationSystemProvider;
        _cardsPerPlayer = CardDistributionUtility.GetCardsPerPlayer(_players.Count);
    }

    public void EnterState()
    {
        Debug.Log($"ACTIVATE STATE: <color=red>{this.GetType()}</color>");

        _cardSpawnerEventsProvider.OnSpawn += OnCardSpawned;

        if(timer != null) Coroutines.Stop(timer);

        timer = DealCardsCoroutine();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        _cardSpawnerEventsProvider.OnSpawn -= OnCardSpawned;

        if (timer != null) Coroutines.Stop(timer);
    }

    private IEnumerator DealCardsCoroutine()
    {
        for (int i = 0; i < _cardsPerPlayer; i++)
        {
            for (int j = 0; j < _players.Count; j++)
            {
                _cardSpawnerProvider.Spawn(_players[j].Id);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    private void OnCardSpawned(int playerIndex, ICard card)
    {
        GetPlayer(playerIndex).AddCard(card);
        _cardsDealt++;

        if (_cardsDealt >= _cardsPerPlayer * _players.Count)
        {
            _cardBankPresentationSystemProvider.MoveToLayout("Normal");

            ChangeStateToPoker();
        }
    }

    private IPlayer GetPlayer(int playerIndex) { return _players.Find(player => player.Id == playerIndex); }

    private void ChangeStateToPoker()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<ChooseCardsPokerState_Game>());
    }
}
