using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;

    public LeaderboardState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - LEADERBOARD STATE / MENU</color>");

        _sceneRoot.OnClickToBack_Leaderboard += ChangeStateToMain;

        _sceneRoot.OpenLeaderboardPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBack_Leaderboard -= ChangeStateToMain;

        _sceneRoot.CloseLeaderboardPanel();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.EnterState(_machineProvider.GetState<MainState_Menu>());
    }
}
