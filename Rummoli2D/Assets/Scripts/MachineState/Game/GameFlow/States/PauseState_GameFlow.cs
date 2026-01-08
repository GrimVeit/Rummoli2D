using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState_GameFlow : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;

    public PauseState_GameFlow(IStateMachineProvider machineProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToResume_Pause += ChangeStateToMain;

        _sceneRoot.OpenPausePanel();
        _sceneRoot.CloseRightPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToResume_Pause -= ChangeStateToMain;

        _sceneRoot.ClosePausePanel();
        _sceneRoot.OpenRightPanel();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.EnterState(_machineProvider.GetState<MainState_GameFlow>());
    }
}
