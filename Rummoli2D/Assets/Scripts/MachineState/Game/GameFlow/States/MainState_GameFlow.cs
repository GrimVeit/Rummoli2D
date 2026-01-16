using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState_GameFlow : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IScoreEarnWinnerListener _scoreEarnWinnerListener;

    public MainState_GameFlow(IStateMachineProvider machineProvider, UIGameRoot sceneRoot, IScoreEarnWinnerListener scoreEarnWinnerListener)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _scoreEarnWinnerListener = scoreEarnWinnerListener;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToPause_Right += ChangeStateToPause;
        _sceneRoot.OnClickToResults_Right += ChangeStateToResults;
        _scoreEarnWinnerListener.OnEndEarn += ChangeStateToFinish;

        _sceneRoot.OpenCardBankPanel();
        _sceneRoot.OpenPlayersPanel();
        _sceneRoot.OpenRummoliPanel();
        _sceneRoot.OpenRoundPanel();
        _sceneRoot.OpenPokerPanel();
        _sceneRoot.OpenRummoliTablePanel();
        _sceneRoot.OpenLeftPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToPause_Right -= ChangeStateToPause;
        _sceneRoot.OnClickToResults_Right -= ChangeStateToResults;
        _scoreEarnWinnerListener.OnEndEarn -= ChangeStateToFinish;

        _sceneRoot.CloseCardBankPanel();
        _sceneRoot.ClosePlayersPanel();
        _sceneRoot.CloseRummoliPanel();
        _sceneRoot.CloseRoundPanel();
        _sceneRoot.ClosePokerPanel();
        _sceneRoot.CloseRummoliTablePanel();
        _sceneRoot.CloseLeftPanel();
    }

    private void ChangeStateToPause()
    {
        _machineProvider.EnterState(_machineProvider.GetState<PauseState_GameFlow>());
    }

    private void ChangeStateToResults()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ResultsState_GameFlow>());
    }

    private void ChangeStateToFinish()
    {
        _machineProvider.EnterState(_machineProvider.GetState<FinishState_GameFlow>());
    }
}
