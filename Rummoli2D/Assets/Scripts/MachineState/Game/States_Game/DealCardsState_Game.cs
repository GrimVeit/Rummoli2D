using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealCardsState_Game : IState
{
    private readonly IStateMachineProvider _stateMachineProvider;
    private readonly List<IPlayer> _players;
    private readonly ICardSpawnerSystemProvider _cardSpawnerProvider;
    private readonly ICardSpawnerSystemEventsProvider _cardSpawnerEventsProvider;
    private readonly int _cardsPerPlayer;
    private int _cardsDealt = 0;

    private IEnumerator timer;

    public DealCardsState_Game(IStateMachineProvider stateMachineProvider, List<IPlayer> players, ICardSpawnerSystemProvider cardSpawnerProvider, ICardSpawnerSystemEventsProvider cardSpawnerEventsProvider)
    {
        _stateMachineProvider = stateMachineProvider;
        _players = players;
        _cardSpawnerProvider = cardSpawnerProvider;
        _cardSpawnerEventsProvider = cardSpawnerEventsProvider;
        _cardsPerPlayer = CardDistributionUtility.GetCardsPerPlayer(_players.Count);
    }

    public void EnterState()
    {
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
        _players[playerIndex].AddCard(card);
        _cardsDealt++;

        if (_cardsDealt >= _cardsPerPlayer * _players.Count)
        {
            ChangeStateToOther();
        }
    }

    private void ChangeStateToOther()
    {

    }
}
