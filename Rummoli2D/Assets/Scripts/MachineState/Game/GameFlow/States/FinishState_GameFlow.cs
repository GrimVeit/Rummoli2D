using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishState_GameFlow : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;

    public FinishState_GameFlow(IStateMachineProvider machineProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.CloseRightPanel();
        _sceneRoot.CloseLeftPanel();
        _sceneRoot.ClosePausePanel();
        _sceneRoot.CloseResultsPanel();
        _sceneRoot.OpenFinishButtonsPanel();
    }

    public void ExitState()
    {

    }
}
