using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;

    public StartState_Game(IStateMachineProvider machineProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToPlay_Start += ChangeStateToShowStartPlayers;

        _sceneRoot.OpenStartPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToPlay_Start -= ChangeStateToShowStartPlayers;

        _sceneRoot.CloseStartPanel();
    }

    private void ChangeStateToShowStartPlayers()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ShowStartPlayersState_Game>());
    }
}
