using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerDoorResultState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IDoorCounterProvider _doorCounterProvider;
    private readonly IDoorStateProvider _doorStateProvider;
    private readonly IVideoProvider _videoProvider;
    private readonly IDoorVisualInfoProvider _doorVisualInfoProvider;

    private readonly IPlayerHealthProvider _playerHealthProvider;
    private readonly ISoundProvider _soundProvider;
    private readonly ISound _soundBackground;
    private Door _currentDoor;

    public DangerDoorResultState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, IDoorCounterProvider doorCounterProvider, IDoorStateProvider doorStateProvider, IVideoProvider videoProvider, IDoorVisualInfoProvider doorVisualInfoProvider, IPlayerHealthProvider playerHealthProvider, ISoundProvider soundProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _doorCounterProvider = doorCounterProvider;
        _doorStateProvider = doorStateProvider;
        _videoProvider = videoProvider;
        _doorVisualInfoProvider = doorVisualInfoProvider;
        _playerHealthProvider = playerHealthProvider;

        _soundProvider = soundProvider;
        _soundBackground = _soundProvider.GetSound("Background");
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE DANGER RESULT STATE</color>");

        _sceneRoot.OpenDoorDangerPanel();

        _currentDoor = _doorVisualInfoProvider.GetCurrentDoor();
        _videoProvider.Play($"DoorDanger_{(int)_currentDoor.DangerLevel}", ChangeStateToCheckWinLose);
        _playerHealthProvider.TakeDamage((int)_currentDoor.DangerLevel);

        _soundBackground.SetVolume(0.3f, 0.1f, 0.2f);
        _soundProvider.PlayOneShot("Danger");
    }

    public void ExitState()
    {
        _doorCounterProvider.AddCount();
        _doorStateProvider.Hide();
        //_sceneRoot.CloseDoorDangerPanel();

        if (_currentDoor.Type == DoorType.Spikes)
            _playerHealthProvider.TakeDamage(1);
    }

    private void ChangeStateToCheckWinLose()
    {
        _machineProvider.SetState(_machineProvider.GetState<CheckWinLoseState_Game>());
    }
}
