using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_Game : IGlobalStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_Game(
        UIGameRoot sceneRoot)
    {
        //states[typeof(StartMainState_Game)] = new StartMainState_Game(this, sceneRoot, doorStateProvider, doorStateEventsProvider, storeDoorProvider);
        //states[typeof(MainState_Game)] = new MainState_Game(this, sceneRoot, doorVisualActivatorProvider, doorVisualEventsProvider, bonusVisualEventsProvider, bonusVisualActivatorProvider, doorVisualInfoProvider, soundProvider);
        //states[typeof(MoveDoorState_Game)] = new MoveDoorState_Game(this, doorVisualInfoProvider, doorStateProvider, doorStateEventsProvider, videoProvider);

        //states[typeof(CheckUseBonusState_Game)] = new CheckUseBonusState_Game(this, bonusApplierInfoProvider);
        //states[typeof(BonusVisibleState_Game)] = new BonusVisibleState_Game(this, bonusApplierProvider, sceneRoot, bonusApplierEventsProvider);
        //states[typeof(BonusTargetState_Game)] = new BonusTargetState_Game(this, bonusApplierProvider, sceneRoot, doorVisualEventsProvider, doorVisualActivatorProvider);

        //states[typeof(NothingDoorResultState_Game)] = new NothingDoorResultState_Game(this, sceneRoot, doorCounterProvider, doorStateProvider, doorVisualInfoProvider, playerHealthProvider, soundProvider);
        //states[typeof(DangerDoorResultState_Game)] = new DangerDoorResultState_Game(this, sceneRoot, doorCounterProvider, doorStateProvider, videoProvider, doorVisualInfoProvider, playerHealthProvider, soundProvider);

        //states[typeof(CheckWinLoseState_Game)] = new CheckWinLoseState_Game(this, doorCounterInfoProvider, playerHealthInfoProvider, sceneRoot, soundProvider);
        //states[typeof(WinState_Game)] = new WinState_Game(this, sceneRoot, videoProvider, bankGameProvider, soundProvider, scoreLaurelProvider);
        //states[typeof(LoseState_Game)] = new LoseState_Game(this, sceneRoot, bankGameProvider, soundProvider);

        //states[typeof(BonusDoorResultState_Game)] = new BonusDoorResultState_Game(this, sceneRoot, doorCounterProvider, doorStateProvider, bonusRewardProvider, doorVisualInfoProvider, soundProvider);
        //states[typeof(BonusRewardState_Game)] = new BonusRewardState_Game(this, sceneRoot, bonusRewardProvider, bonusRewardEventsProvider, bonusRewardInfoProvider);
    }

    public void Initialize()
    {
        //SetState(GetState<StartMainState_Game>());
    }

    public void Dispose()
    {

    }

    public IState GetState<T>() where T : IState
    {
        return states[typeof(T)];
    }

    public void SetState(IState state)
    {
        _currentState?.ExitState();

        _currentState = state;
        _currentState.EnterState();
    }
}
