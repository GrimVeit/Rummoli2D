using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProgressState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIMainMenuRoot _sceneRoot;
    private readonly IResetProgressBarProvider _resetProgressBarProvider;
    private readonly ITextEffectHideShowActivator _textEffectHideShowActivator;
    private readonly IStoreProgressScoreProvider _storeProgressScoreProvider;

    public ResetProgressState_Menu(IStateMachineProvider machineProvider, UIMainMenuRoot sceneRoot, IResetProgressBarProvider resetProgressBarProvider, ITextEffectHideShowActivator textEffectHideShowActivator, IStoreProgressScoreProvider storeProgressScoreProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _resetProgressBarProvider = resetProgressBarProvider;
        _textEffectHideShowActivator = textEffectHideShowActivator;
        _storeProgressScoreProvider = storeProgressScoreProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - PROFILE STATE / MENU</color>");

        _sceneRoot.OnClickToBack_ResetProgress += ChangeStateToSettings;
        _sceneRoot.OnClickToReset_ResetProgress += Reset;

        _sceneRoot.OpenResetProgressPanel();
        _textEffectHideShowActivator.ActivateVisual(0.4f);
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBack_ResetProgress -= ChangeStateToSettings;
        _sceneRoot.OnClickToReset_ResetProgress -= Reset;

        _sceneRoot.CloseResetProgressPanel();
        _textEffectHideShowActivator.DeactivateVisual(0.4f);
    }

    private void Reset()
    {
        _storeProgressScoreProvider.ResetScoreProgress();

        ChangeStateToSettings();
    }

    private void ChangeStateToSettings()
    {
        _resetProgressBarProvider.Reset();

        _machineProvider.EnterState(_machineProvider.GetState<SettingsState_Menu>());
    }
}
