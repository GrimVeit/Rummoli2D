using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NothingDoorResultState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IDoorCounterProvider _doorCounterProvider;
    private readonly IDoorStateProvider _doorStateProvider;

    private readonly IDoorVisualInfoProvider _visualInfoProvider;
    private readonly IPlayerHealthProvider _healthProvider;
    private readonly ISoundProvider _soundProvider;
    private readonly ISound _soundBackground;

    private IEnumerator timer;

    public NothingDoorResultState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, IDoorCounterProvider doorCounterProvider, IDoorStateProvider doorStateProvider, IDoorVisualInfoProvider doorVisualInfoProvider, IPlayerHealthProvider playerHealthProvider, ISoundProvider soundProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _doorCounterProvider = doorCounterProvider;
        _doorStateProvider = doorStateProvider;
        _visualInfoProvider = doorVisualInfoProvider;
        _healthProvider = playerHealthProvider;

        _soundProvider = soundProvider;
        _soundBackground = _soundProvider.GetSound("Background");
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE NOTHING RESULT STATE</color>");

        _sceneRoot.OpenDoorNothingPanel();
        _sceneRoot.OpenDoorNothingBackgroundPanel();

        if(timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);

        _soundBackground.SetVolume(0.3f, 0.1f, 0.2f);
        _soundProvider.PlayOneShot("Fog");
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);

        _doorCounterProvider.AddCount();
        _doorStateProvider.Hide();

        _sceneRoot.CloseDoorNothingPanel();
        _sceneRoot.CloseDoorNothingBaackgroundPanel();

        //DAMAGE FROM SPIKES
        var door = _visualInfoProvider.GetCurrentDoor();
        if (door.Type == DoorType.Spikes)
            _healthProvider.TakeDamage(1);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);

        ChangeStateToCheckWinLose();
    }

    private void ChangeStateToCheckWinLose()
    {
        _machineProvider.SetState(_machineProvider.GetState<CheckWinLoseState_Game>());
    }
}
