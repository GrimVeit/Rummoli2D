using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_GameFlow : IStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_GameFlow
        (UIGameRoot sceneRoot,
        IGameInfoVisualActivater gameInfoVisualActivater)
    {
        states[typeof(StartState_GameFlow)] = new StartState_GameFlow(this, sceneRoot, gameInfoVisualActivater);
        states[typeof(MainState_GameFlow)] = new MainState_GameFlow(this, sceneRoot);
        states[typeof(PauseState_GameFlow)] = new PauseState_GameFlow(this, sceneRoot);
    }

    public void Initialize()
    {
        EnterState(GetState<StartState_GameFlow>());
    }

    public void Dispose()
    {
        _currentState?.ExitState();
    }

    public IState GetState<T>() where T : IState
    {
        return states[typeof(T)];
    }

    public void EnterState(IState state)
    {
        _currentState?.ExitState();

        _currentState = state;
        _currentState.EnterState();
    }
}
