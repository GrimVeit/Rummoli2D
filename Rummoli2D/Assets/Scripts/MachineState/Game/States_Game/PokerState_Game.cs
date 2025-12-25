using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly IPlayerPokerProvider _playerPokerProvider;
    private readonly IPlayerPresentationSystemProvider _playerPresentationSystemProvider;
    private readonly List<IPlayer> _players;

    public PokerState_Game(IStateMachineProvider machineProvider, List<IPlayer> players, IPlayerPokerProvider playerPokerProvider, IPlayerPresentationSystemProvider playerPresentationSystemProvider)
    {
        _machineProvider = machineProvider;
        _players = players;
        _playerPokerProvider = playerPokerProvider;
        _playerPresentationSystemProvider = playerPresentationSystemProvider;
    }

    public void EnterState()
    {
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
    }
}
