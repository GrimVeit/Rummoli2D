using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;

    public ProfileState_Menu(IGlobalStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - PROFILE STATE / MENU</color>");

        _sceneRoot.OnClickToBack_Profile += ChangeStateToMain;

        _sceneRoot.OpenBackgroundSecondPanel();
        _sceneRoot.OpenProfilePanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBack_Profile -= ChangeStateToMain;

        _sceneRoot.CloseBackgroundSecondPanel();
        _sceneRoot.CloseProfilePanel();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Menu>());
    }
}
