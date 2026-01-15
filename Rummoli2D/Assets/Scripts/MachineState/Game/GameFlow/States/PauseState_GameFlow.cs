using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState_GameFlow : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IHintSystemActivatorProvider _hintSystemActivatorProvider;
    private readonly ITextEffectHideShowActivator _textEffectHideShowActivator;

    public PauseState_GameFlow(IStateMachineProvider machineProvider, UIGameRoot sceneRoot, IHintSystemActivatorProvider hintSystemActivatorProvider, ITextEffectHideShowActivator textEffectHideShowActivator)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _hintSystemActivatorProvider = hintSystemActivatorProvider;
        _textEffectHideShowActivator = textEffectHideShowActivator;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToResume_Pause += ChangeStateToMain;

        _sceneRoot.OpenPausePanel();
        _sceneRoot.CloseRightPanel();
        _hintSystemActivatorProvider.HideAll();
        _textEffectHideShowActivator.ActivateVisual(0.4f);
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToResume_Pause -= ChangeStateToMain;

        _sceneRoot.ClosePausePanel();
        _sceneRoot.OpenRightPanel();
        _hintSystemActivatorProvider.ShowAll();
        _textEffectHideShowActivator.DeactivateVisual(0.4f);
    }

    private void ChangeStateToMain()
    {
        _machineProvider.EnterState(_machineProvider.GetState<MainState_GameFlow>());
    }
}
