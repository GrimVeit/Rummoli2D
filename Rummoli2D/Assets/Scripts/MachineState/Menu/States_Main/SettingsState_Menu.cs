using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;
    private readonly IResetProgressBarListener _resetProgressBarListener;

    public SettingsState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot, IResetProgressBarListener resetProgressBarListener)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _resetProgressBarListener = resetProgressBarListener;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - PROFILE STATE / MENU</color>");

        _resetProgressBarListener.OnActivateReset += ChangeStateToResetProgress;
        _sceneRoot.OnClickToBack_Settings += ChangeStateToMain;

        _sceneRoot.OpenSettingsPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBack_Settings -= ChangeStateToMain;
        _resetProgressBarListener.OnActivateReset -= ChangeStateToResetProgress;

        _sceneRoot.CloseSettingsPanel();
    }

    private void ChangeStateToMain()
    {
        _machineProvider.EnterState(_machineProvider.GetState<MainState_Menu>());
    }

    private void ChangeStateToResetProgress()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ResetProgressState_Menu>());
    }
}
