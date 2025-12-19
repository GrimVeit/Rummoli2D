using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;

    public BalanceState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - BALANCE STATE / MENU</color>");

        _sceneRoot.OnClickToShop_Balance += ChangeStateToShop;
        _sceneRoot.OnClickToBack_Balance += ChangeStateToMain;

        _sceneRoot.OpenBalancePanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToShop_Balance -= ChangeStateToShop;
        _sceneRoot.OnClickToBack_Balance -= ChangeStateToMain;

        _sceneRoot.CloseBalancePanel();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.EnterState(_machineProvider.GetState<MainState_Menu>());
    }

    private void ChangeStateToShop()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ShopState_Menu>());
    }
}
