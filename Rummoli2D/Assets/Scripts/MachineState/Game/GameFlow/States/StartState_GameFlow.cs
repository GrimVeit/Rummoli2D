using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState_GameFlow : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IGameInfoVisualActivater _gameInfoVisualActivater;

    public StartState_GameFlow(IStateMachineProvider machineProvider, UIGameRoot sceneRoot, IGameInfoVisualActivater gameInfoVisualActivater)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _gameInfoVisualActivater = gameInfoVisualActivater;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToPlay_Start += ChangeStateToMain;

        _sceneRoot.OpenCardBankPanel();
        _sceneRoot.OpenPlayersPanel();
        _sceneRoot.OpenRummoliPanel();
        _sceneRoot.OpenRummoliTablePanel();
        _sceneRoot.OpenStartPanel();

        _gameInfoVisualActivater.ActivateVisual();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToPlay_Start -= ChangeStateToMain;

        _sceneRoot.CloseStartPanel();

        _gameInfoVisualActivater.DeactivateVisual();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.EnterState(_machineProvider.GetState<MainState_GameFlow>());
    }
}
