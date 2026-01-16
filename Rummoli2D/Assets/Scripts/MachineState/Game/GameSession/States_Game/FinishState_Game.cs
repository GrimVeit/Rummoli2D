using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FinishState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IScoreEarnWinnerProvider _scoreEarnWinnerProvider;

    private IEnumerator timer;

    public FinishState_Game(IStateMachineProvider machineProvider, UIGameRoot sceneRoot, IScoreEarnWinnerProvider scoreEarnWinnerProvider) 
    { 
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _scoreEarnWinnerProvider = scoreEarnWinnerProvider;
    }

    public void EnterState()
    {
        if (timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);
    }

    private IEnumerator Timer()
    {
        _sceneRoot.OpenFinishResultsPanel();

        yield return new WaitForSeconds(1f);

        _scoreEarnWinnerProvider.SearchWinners();
    }
}
