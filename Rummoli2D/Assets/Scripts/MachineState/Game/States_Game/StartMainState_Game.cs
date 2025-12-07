using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMainState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IDoorStateProvider _doorStateProvider;
    private readonly IDoorStateEventsProvider _doorStateEventsProvider;
    private readonly IStoreDoorProvider _storeDoorProvider;

    public StartMainState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, IDoorStateProvider doorStateProvider, IDoorStateEventsProvider doorStateEventsProvider, IStoreDoorProvider storeDoorProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _doorStateProvider = doorStateProvider;
        _doorStateEventsProvider = doorStateEventsProvider;
        _storeDoorProvider = storeDoorProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE START MAIN STATE</color>");

        _doorStateEventsProvider.OnEndActivateAllDoors += ChangeStateToMain;

        _sceneRoot.OpenMainPanel();
        _sceneRoot.OpenFooterPanel();

        _storeDoorProvider.GenerateDoors();
        _doorStateProvider.ActivateAll();
    }

    public void ExitState()
    {
        _doorStateEventsProvider.OnEndActivateAllDoors -= ChangeStateToMain;
    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Game>());
    }
}
