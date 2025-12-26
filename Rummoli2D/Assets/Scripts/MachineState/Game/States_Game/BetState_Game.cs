using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;

    private readonly List<IPlayer> _players;
    private readonly int _startIndex;

    private int _currentIndex;
    private int _completedCount;

    public BetState_Game(IStateMachineProvider machineProvider, List<IPlayer> players)
    {
        _machineProvider = machineProvider;
        _players = players;
        _startIndex = Random.Range(0, _players.Count);
    }

    public void EnterState()
    {
        Debug.Log($"ACTIVATE STATE: <color=red>{this.GetType()}</color>");
        _completedCount = 0;
        _currentIndex = _startIndex;

        ActivateCurrentPlayer();
    }

    public void ExitState()
    {
        DeactivateCurrentPlayer();
    }

    private void ActivateCurrentPlayer()
    {
        var player = _players[_currentIndex];

        Debug.Log("Activate Player with Index = " +  player.Id);

        player.OnApplyBet += OnPlayerAppliedBet;
        player.ActivateApplyBet();
    }

    private void DeactivateCurrentPlayer()
    {
        var player = _players[_currentIndex];

        Debug.Log("Deactivate Player with Index = " + player.Id);

        player.OnApplyBet -= OnPlayerAppliedBet;
        player.DeactivateApplyBet();
    }

    private void OnPlayerAppliedBet()
    {
        DeactivateCurrentPlayer();

        _completedCount++;

        if (_completedCount >= _players.Count)
        {
            ChangeToMovePlayerPeopleToGameState();
            return;
        }

        MoveToNextPlayer();
        ActivateCurrentPlayer();
    }

    private void MoveToNextPlayer()
    {
        _currentIndex++;

        if (_currentIndex >= _players.Count)
            _currentIndex = 0;
    }

    private void ChangeToMovePlayerPeopleToGameState()
    {
        _machineProvider.EnterState(_machineProvider.GetState<MovePlayerPeopleToGameState_Game>());
    }
}
