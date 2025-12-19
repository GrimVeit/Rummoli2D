using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;

    public SettingsState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - PROFILE STATE / MENU</color>");

        _sceneRoot.OnClickToBack_Settings += ChangeStateToMain;

        _sceneRoot.OpenSettingsPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBack_Settings -= ChangeStateToMain;

        _sceneRoot.CloseSettingsPanel();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.EnterState(_machineProvider.GetState<MainState_Menu>());
    }
}
