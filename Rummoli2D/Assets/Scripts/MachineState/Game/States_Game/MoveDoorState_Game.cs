using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDoorState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly IDoorVisualInfoProvider _doorVisualInfoProvider;
    private readonly IDoorStateProvider _doorStateProvider;
    private readonly IDoorStateEventsProvider _doorStateEventsProvider;
    private readonly IVideoProvider _videoProvider;

    private Door _currentDoor;

    public MoveDoorState_Game(IGlobalStateMachineProvider machineProvider, IDoorVisualInfoProvider doorVisualInfoProvider, IDoorStateProvider doorStateProvider, IDoorStateEventsProvider doorStateEventsProvider, IVideoProvider videoProvider)
    {
        _machineProvider = machineProvider;
        _doorVisualInfoProvider = doorVisualInfoProvider;
        _doorStateProvider = doorStateProvider;
        _doorStateEventsProvider = doorStateEventsProvider;
        _videoProvider = videoProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE MOVE DOOR STATE</color>");

        _doorStateEventsProvider.OnEndOpenDoor += ChooseDoor;

        var indexDoor = _doorVisualInfoProvider.GetCurrentIndexDoor();
        _doorStateProvider.OpenDoor(indexDoor);
        _currentDoor = _doorVisualInfoProvider.GetCurrentDoor();

        if (_currentDoor.HasDanger)
        {
            _videoProvider.Prepare($"DoorDanger_{(int)_currentDoor.DangerLevel}");
        }
    }

    public void ExitState()
    {
        _doorStateEventsProvider.OnEndOpenDoor -= ChooseDoor;
    }

    private void ChooseDoor()
    {
        if (_currentDoor.HasDanger)
        {
            ChangeStateToDanger();
        }
        else if (_currentDoor.HasBonus && !_currentDoor.HasDanger)
        {
            ChangeStateToBonus();
        }
        else if (!_currentDoor.HasDanger && !_currentDoor.HasBonus)
        {
            ChangeStateToNothing();
        }
    }

    private void ChangeStateToNothing()
    {
        _machineProvider.SetState(_machineProvider.GetState<NothingDoorResultState_Game>());
    }

    private void ChangeStateToBonus()
    {
        _machineProvider.SetState(_machineProvider.GetState<BonusDoorResultState_Game>());
    }

    private void ChangeStateToDanger()
    {
        _machineProvider.SetState(_machineProvider.GetState<DangerDoorResultState_Game>());
    }
}
