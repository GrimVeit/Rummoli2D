using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly IScoreEarnLeaderboardProvider _scoreEarnLeaderboardProvider;
    private readonly List<IPlayer> _players;

    private IEnumerator timer;

    public StartState_Game(IStateMachineProvider machineProvider, IScoreEarnLeaderboardProvider scoreEarnLeaderboardProvider, List<IPlayer> players)
    {
        _machineProvider = machineProvider;
        _scoreEarnLeaderboardProvider = scoreEarnLeaderboardProvider;
        _players = players;
    }

    public void EnterState()
    {
        _scoreEarnLeaderboardProvider.RegisterPlayers(_players);

        if(timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);

        ChangeStateToShowStartPlayers();
    }

    private void ChangeStateToShowStartPlayers()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ShowStartPlayersState_Game>());
    }
}
