using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsState_GameFlow : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IHintSystemActivatorProvider _hintSystemActivatorProvider;

    public ResultsState_GameFlow(IStateMachineProvider machineProvider, UIGameRoot sceneRoot, IHintSystemActivatorProvider hintSystemActivatorProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _hintSystemActivatorProvider = hintSystemActivatorProvider;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToResume_Results += ChangeStateToMain;

        _sceneRoot.OpenResultsPanel();
        _sceneRoot.CloseRightPanel();
        _hintSystemActivatorProvider.HideAll();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToResume_Results -= ChangeStateToMain;

        _sceneRoot.CloseResultsPanel();
        _sceneRoot.OpenRightPanel();
        _hintSystemActivatorProvider.ShowAll();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.EnterState(_machineProvider.GetState<MainState_GameFlow>());
    }
}
