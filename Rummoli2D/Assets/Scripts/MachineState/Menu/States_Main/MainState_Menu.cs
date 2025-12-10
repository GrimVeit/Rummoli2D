using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;

    public MainState_Menu(IGlobalStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - MAIN STATE / MENU</color>");

        _sceneRoot.OnClickToRules_Main += ChangeStateToRules;
        _sceneRoot.OnClickToProfile_Main += ChangeStateToProfile;
        _sceneRoot.OnClickToBalance_Main += ChangeStateToBalance;
        _sceneRoot.OnClickToSettings_Main += ChangeStateToSettings;
        _sceneRoot.OnClickToShop_Main += ChangeStateToShop;

        _sceneRoot.OpenMainPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToRules_Main -= ChangeStateToRules;
        _sceneRoot.OnClickToProfile_Main -= ChangeStateToProfile;
        _sceneRoot.OnClickToBalance_Main -= ChangeStateToBalance;
        _sceneRoot.OnClickToSettings_Main -= ChangeStateToSettings;
        _sceneRoot.OnClickToShop_Main += ChangeStateToShop;

        _sceneRoot.CloseMainPanel();
    }

    private void ChangeStateToRules()
    {
        _machineProvider.SetState(_machineProvider.GetState<RulesState_Menu>());
    }

    private void ChangeStateToProfile()
    {
        _machineProvider.SetState(_machineProvider.GetState<ProfileState_Menu>());
    }

    private void ChangeStateToBalance()
    {

    }

    private void ChangeStateToSettings()
    {

    }

    private void ChangeStateToShop()
    {
        _machineProvider.SetState(_machineProvider.GetState<ShopState_Menu>());
    }
}
