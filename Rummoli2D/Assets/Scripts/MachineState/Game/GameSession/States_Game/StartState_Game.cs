using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IScoreEarnLeaderboardProvider _scoreEarnLeaderboardProvider;
    private readonly List<IPlayer> _players;

    public StartState_Game(IStateMachineProvider machineProvider, UIGameRoot sceneRoot, IScoreEarnLeaderboardProvider scoreEarnLeaderboardProvider, List<IPlayer> players)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _scoreEarnLeaderboardProvider = scoreEarnLeaderboardProvider;
        _players = players;
    }

    public void EnterState()
    {
        _scoreEarnLeaderboardProvider.RegisterPlayers(_players);

        _sceneRoot.OnClickToPlay_Start += ChangeStateToShowStartPlayers;
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToPlay_Start -= ChangeStateToShowStartPlayers;
    }

    private void ChangeStateToShowStartPlayers()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ShowStartPlayersState_Game>());
    }
}
