using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTargetState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly IBonusApplierProvider _bonusApplierProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IDoorVisualEventsProvider _doorVisualEventsProvider;
    private readonly IDoorVisualActivatorProvider _doorVisualActivatorProvider;

    public BonusTargetState_Game(IGlobalStateMachineProvider machineProvider, IBonusApplierProvider bonusApplierProvider, UIGameRoot sceneRoot, IDoorVisualEventsProvider doorVisualEventsProvider, IDoorVisualActivatorProvider doorVisualActivatorProvider)
    {
        _machineProvider = machineProvider;
        _bonusApplierProvider = bonusApplierProvider;
        _sceneRoot = sceneRoot;
        _doorVisualEventsProvider = doorVisualEventsProvider;
        _doorVisualActivatorProvider = doorVisualActivatorProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE BONUS TARGET STATE</color>");

        _doorVisualEventsProvider.OnChooseDoor_Index += _bonusApplierProvider.ApplyBonus;
        _doorVisualEventsProvider.OnChooseDoor += ChangeStateToMainMenu;

        _doorVisualActivatorProvider.ActivateInteraction();
        _sceneRoot.CloseMainPanel();
        _sceneRoot.OpenChooseDoorForApplyPanel();
    }

    public void ExitState()
    {
        _doorVisualEventsProvider.OnChooseDoor_Index -= _bonusApplierProvider.ApplyBonus;
        _doorVisualEventsProvider.OnChooseDoor -= ChangeStateToMainMenu;

        _doorVisualActivatorProvider.DeactivateInteraction();
        _sceneRoot.OpenMainPanel();
        _sceneRoot.OpenFooterPanel();
        _sceneRoot.CloseChooseDoorForApplyPanel();
    }

    private void ChangeStateToMainMenu()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Game>());
    }
}
