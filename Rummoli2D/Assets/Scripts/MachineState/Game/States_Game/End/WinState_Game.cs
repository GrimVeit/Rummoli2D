using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IVideoProvider _videoProvider;
    private readonly IBankGameProvider _bankGameProvider;
    private readonly ISoundProvider _soundProvider;
    private readonly IScoreLaurelProvider _scoreLaurelProvider;

    private IEnumerator timer;

    public WinState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, IVideoProvider videoProvider, IBankGameProvider bankGameProvider, ISoundProvider soundProvider, IScoreLaurelProvider scoreLaurelProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _videoProvider = videoProvider;
        _bankGameProvider = bankGameProvider;
        _soundProvider = soundProvider;
        _scoreLaurelProvider = scoreLaurelProvider;
    }

    public void EnterState()
    {
        if(timer != null) Coroutines.Stop(timer);

        timer = Timer(5f);
        Coroutines.Start(timer);

        _scoreLaurelProvider.AddLaurel();
        _soundProvider.PlayOneShot("Win");
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);
    }

    private IEnumerator Timer(float timeWait)
    {
        _sceneRoot.OpenWinPanel();
        _videoProvider.Play("Win");

        yield return new WaitForSeconds(timeWait);

        _bankGameProvider.ApplyWinBonus();
    }
}
