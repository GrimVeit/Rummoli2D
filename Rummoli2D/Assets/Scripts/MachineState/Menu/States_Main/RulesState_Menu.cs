using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;
    private readonly IRulesVisualProvider _rulesVisualProvider;

    public RulesState_Menu(IGlobalStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot, IRulesVisualProvider rulesVisualProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _rulesVisualProvider = rulesVisualProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - PROFILE STATE / MENU</color>");

        _sceneRoot.OnClickToBack_Rules += ChangeStateToMain;

        _sceneRoot.OpenRulesPanel();
        _rulesVisualProvider.ResetPage();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBack_Rules -= ChangeStateToMain;

        _sceneRoot.CloseRulesPanel();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Menu>());
    }
}
