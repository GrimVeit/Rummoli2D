using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;
    private readonly ITextEffectHideShowActivator _textEffectHideShowActivator;

    public NewGameState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot, ITextEffectHideShowActivator textEffectHideShowActivator)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _textEffectHideShowActivator = textEffectHideShowActivator;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - NEW GAME STATE / MENU</color>");

        _sceneRoot.OnClickToBack_NewGame += ChangeStateToMain;

        _sceneRoot.OpenNewGamePanel();
        _textEffectHideShowActivator.ActivateVisual(0.5f);
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBack_NewGame -= ChangeStateToMain;

        _sceneRoot.CloseNewGamePanel();
        _textEffectHideShowActivator.DeactivateVisual(0.5f);
    }

    private void ChangeStateToMain()
    {
        _machineProvider.EnterState(_machineProvider.GetState<MainState_Menu>());
    }
}
