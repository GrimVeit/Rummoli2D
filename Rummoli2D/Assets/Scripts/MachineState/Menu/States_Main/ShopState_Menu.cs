using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;
    private readonly IShopScrollProvider _shopScrollProvider;

    public ShopState_Menu(IGlobalStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot, IShopScrollProvider shopScrollProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _shopScrollProvider = shopScrollProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - SHOP STATE / MENU</color>");

        _sceneRoot.OnClickToBack_Shop += ChangeStateToMain;

        _sceneRoot.OpenShopPanel();

        _shopScrollProvider.ResetPage();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBack_Shop -= ChangeStateToMain;

        _sceneRoot.CloseShopPanel();

        _shopScrollProvider.CloseAllPage();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Menu>());
    }
}
