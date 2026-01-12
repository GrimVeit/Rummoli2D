using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsState_GameFlow : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;

    public ResultsState_GameFlow(IStateMachineProvider machineProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToResume_Results += ChangeStateToMain;

        _sceneRoot.OpenResultsPanel();
        _sceneRoot.CloseRightPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToResume_Results -= ChangeStateToMain;

        _sceneRoot.CloseResultsPanel();
        _sceneRoot.OpenRightPanel();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.EnterState(_machineProvider.GetState<MainState_GameFlow>());
    }
}
