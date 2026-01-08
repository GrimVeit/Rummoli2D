using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCardsPokerState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly IPlayerPokerProvider _playerPokerProvider;
    private readonly IPlayerPresentationSystemProvider _playerPresentationSystemProvider;

    private int _currentCount = 0;
    private readonly List<IPlayer> _players;

    public ChooseCardsPokerState_Game(IStateMachineProvider machineProvider, List<IPlayer> players, IPlayerPokerProvider playerPokerProvider, IPlayerPresentationSystemProvider playerPresentationSystemProvider)
    {
        _machineProvider = machineProvider;
        _players = players;
        _playerPokerProvider = playerPokerProvider;
        _playerPresentationSystemProvider = playerPresentationSystemProvider;
    }

    public void EnterState()
    {
        _currentCount = 0;

        Debug.Log($"ACTIVATE STATE: <color=red>{this.GetType()}</color>");

        _playerPokerProvider.SetCountPlayer(_players.Count);

        for (int i = 0; i < _players.Count; i++)
        {
            _players[i].OnChoose5Cards += OnChoose5Cards;

            _players[i].ActiveChoose5Cards();
        }
    }

    public void ExitState()
    {

    }

    private void OnChoose5Cards(IPlayer player, List<ICard> cards)
    {
        player.OnChoose5Cards -= OnChoose5Cards;
        player.DeactivateChoose5Cards();

        _playerPresentationSystemProvider.Hide(player.Id);

        _playerPokerProvider.SetPlayer(player.Id, player.Name, cards);

        _currentCount += 1;

        if(_currentCount == _players.Count)
        {
            ChangeStateToResultPokerState();
        }
    }

    private void ChangeStateToResultPokerState()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ResultPokerState_Game>());
    }
}
