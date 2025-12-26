using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPokerState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly IPlayerPokerProvider _playerPokerProvider;
    private readonly IPlayerPokerListener _playerPokerListener;
    private readonly IPlayerPresentationSystemProvider _playerPresentationSystemProvider;

    private readonly List<IPlayer> _players;
    private  int _winnerPlayerId = -1;

    private IEnumerator timer;

    public ResultPokerState_Game(IStateMachineProvider machineProvider, List<IPlayer> players, IPlayerPokerProvider playerPokerProvider, IPlayerPresentationSystemProvider playerPresentationSystemProvider, IPlayerPokerListener playerPokerListener)
    {
        _machineProvider = machineProvider;
        _players = players;
        _playerPokerProvider = playerPokerProvider;
        _playerPresentationSystemProvider = playerPresentationSystemProvider;
        _playerPokerListener = playerPokerListener;
    }

    public void EnterState()
    {
        _playerPokerListener.OnSearchWinner += SetWinner;

        Debug.Log($"ACTIVATE STATE: <color=red>{this.GetType()}</color>");

        if(timer != null ) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        _playerPokerListener.OnSearchWinner -= SetWinner;

        if (timer != null) Coroutines.Stop(timer);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.2f);

        _playerPokerProvider.ShowTable();

        yield return new WaitForSeconds(1f);

        _playerPokerProvider.ShowAll();

        yield return new WaitForSeconds(4f);

        _playerPokerProvider.SearchWinner();

        yield return new WaitForSeconds(0.3f);

        _playerPokerProvider.ClearAll();

    }

    private void SetWinner(int playerId)
    {
        _winnerPlayerId = playerId;
    }
}
