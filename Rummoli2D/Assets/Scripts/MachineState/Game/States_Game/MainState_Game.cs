using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IDoorVisualActivatorProvider _doorVisualActivatorProvider;
    private readonly IDoorVisualEventsProvider _doorVisualEventsProvider;
    private readonly IBonusVisualEventsProvider _bonusVisualEventsProvider;
    private readonly IBonusVisualActivatorProvider _bonusVisualActivatorProvider;
    private readonly IDoorVisualInfoProvider _doorVisualInfoProvider;
    private readonly ISoundProvider _soundProvider;

    public MainState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, IDoorVisualActivatorProvider doorVisualActivatorProvider, IDoorVisualEventsProvider doorVisualEventsProvider, IBonusVisualEventsProvider bonusVisualEventsProvider, IBonusVisualActivatorProvider bonusVisualActivatorProvider, IDoorVisualInfoProvider doorVisualInfoProvider, ISoundProvider soundProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _doorVisualActivatorProvider = doorVisualActivatorProvider;
        _doorVisualEventsProvider = doorVisualEventsProvider;
        _bonusVisualEventsProvider = bonusVisualEventsProvider;
        _bonusVisualActivatorProvider = bonusVisualActivatorProvider;
        _doorVisualInfoProvider = doorVisualInfoProvider;
        _soundProvider = soundProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE MAIN STATE</color>");

        _doorVisualEventsProvider.OnChooseDoor += ChangeStateToDoorMove;
        _bonusVisualEventsProvider.OnChooseBonus += ChangeStateToCheckUseBonus;

        _doorVisualActivatorProvider.ActivateInteraction();
        _bonusVisualActivatorProvider.ActivateInteraction();
    }

    public void ExitState()
    {
        _doorVisualEventsProvider.OnChooseDoor -= ChangeStateToDoorMove;
        _bonusVisualEventsProvider.OnChooseBonus -= ChangeStateToCheckUseBonus;

        _doorVisualActivatorProvider.DeactivateInteraction();
        _bonusVisualActivatorProvider.DeactivateInteraction();

        _sceneRoot.CloseFooterPanel();
    }
    
    private void ChangeStateToDoorMove()
    {
        if (_doorVisualInfoProvider.GetCurrentDoor().HasLock)
        {
            Debug.Log("LOCKED!!!");
            _soundProvider.PlayOneShot("DoorLock");
            return;
        }

        _sceneRoot.CloseMainPanel();

        _machineProvider.SetState(_machineProvider.GetState<MoveDoorState_Game>());
    }

    private void ChangeStateToCheckUseBonus()
    {
        _machineProvider.SetState(_machineProvider.GetState<CheckUseBonusState_Game>());
    }
}
