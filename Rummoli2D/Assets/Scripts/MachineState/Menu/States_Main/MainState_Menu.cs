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

        _sceneRoot.OnClickToLeaderboard_Main += ChangeStateToLeaderboard;
        _sceneRoot.OnClickToProfile_Main += ChangeStateToProfile;
        _sceneRoot.OnClickToShop_Main += ChangeStateToShop;

        _sceneRoot.OpenBackgroundMainPanel();
        _sceneRoot.OpenMainPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToLeaderboard_Main -= ChangeStateToLeaderboard;
        _sceneRoot.OnClickToProfile_Main -= ChangeStateToProfile;
        _sceneRoot.OnClickToShop_Main -= ChangeStateToShop;

        _sceneRoot.CloseBackgroundMainPanel();
        _sceneRoot.CloseMainPanel();
    }

    private void ChangeStateToLeaderboard()
    {
        _machineProvider.SetState(_machineProvider.GetState<LeaderboardState_Menu>());
    }

    private void ChangeStateToProfile()
    {
        _machineProvider.SetState(_machineProvider.GetState<ProfileState_Menu>());
    }

    private void ChangeStateToShop()
    {
        _machineProvider.SetState(_machineProvider.GetState<ShopState_Menu>());
    }
}
