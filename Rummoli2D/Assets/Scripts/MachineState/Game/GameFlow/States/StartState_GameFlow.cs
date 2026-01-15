using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState_GameFlow : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;

    public StartState_GameFlow(IStateMachineProvider machineProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OpenCardBankPanel();
        _sceneRoot.OpenPlayersPanel();
        _sceneRoot.OpenRummoliPanel();
        _sceneRoot.OpenRummoliTablePanel();

        ChangeStateToMain();
    }

    public void ExitState()
    {

    }

    private void ChangeStateToMain()
    {
        _machineProvider.EnterState(_machineProvider.GetState<MainState_GameFlow>());
    }
}
