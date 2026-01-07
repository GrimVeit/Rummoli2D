using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IGameInfoVisualActivater _gameInfoVisualActivater;

    public StartState_Game(IStateMachineProvider machineProvider, UIGameRoot sceneRoot, IGameInfoVisualActivater gameInfoVisualActivater)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _gameInfoVisualActivater = gameInfoVisualActivater;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToPlay_Start += ChangeStateToShowStartPlayers;

        _sceneRoot.OpenStartPanel();

        _gameInfoVisualActivater.ActivateVisual();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToPlay_Start -= ChangeStateToShowStartPlayers;

        _sceneRoot.CloseStartPanel();

        _gameInfoVisualActivater.DeactivateVisual();
    }

    private void ChangeStateToShowStartPlayers()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ShowStartPlayersState_Game>());
    }
}
