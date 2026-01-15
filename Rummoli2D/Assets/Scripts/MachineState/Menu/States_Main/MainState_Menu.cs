using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;

    public MainState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot)
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
        _sceneRoot.OnClickToNewGame_Main += ChangeStateToNewGame;

        _sceneRoot.OpenMainPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToRules_Main -= ChangeStateToRules;
        _sceneRoot.OnClickToProfile_Main -= ChangeStateToProfile;
        _sceneRoot.OnClickToBalance_Main -= ChangeStateToBalance;
        _sceneRoot.OnClickToSettings_Main -= ChangeStateToSettings;
        _sceneRoot.OnClickToShop_Main += ChangeStateToShop;
        _sceneRoot.OnClickToNewGame_Main -= ChangeStateToNewGame;

        _sceneRoot.CloseMainPanel();
    }

    private void ChangeStateToNewGame()
    {
        _machineProvider.EnterState(_machineProvider.GetState<NewGameState_Menu>());
    }

    private void ChangeStateToRules()
    {
        _machineProvider.EnterState(_machineProvider.GetState<RulesState_Menu>());
    }

    private void ChangeStateToProfile()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ProfileState_Menu>());
    }

    private void ChangeStateToBalance()
    {
        _machineProvider.EnterState(_machineProvider.GetState<BalanceState_Menu>());
    }

    private void ChangeStateToSettings()
    {
        _machineProvider.EnterState(_machineProvider.GetState<SettingsState_Menu>());
    }

    private void ChangeStateToShop()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ShopState_Menu>());
    }
}
