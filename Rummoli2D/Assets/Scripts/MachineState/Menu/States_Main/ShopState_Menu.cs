using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;

    public ShopState_Menu(IGlobalStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - SHOP STATE / MENU</color>");

        _sceneRoot.OnClickToBack_Shop += ChangeStateToMain;

        _sceneRoot.OpenBackgroundSecondPanel();
        _sceneRoot.OpenShopPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBack_Shop -= ChangeStateToMain;

        _sceneRoot.CloseBackgroundSecondPanel();
        _sceneRoot.CloseShopPanel();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Menu>());
    }
}
