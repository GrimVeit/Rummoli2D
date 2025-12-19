using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_Game : IStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_Game(List<IPlayer> players)
    {
        states[typeof(StartingBalanceState_Game)] = new StartingBalanceState_Game(this, players);
        states[typeof(BetState_Game)] = new BetState_Game(players);
    }

    public void Initialize()
    {
        EnterState(GetState<StartingBalanceState_Game>());
    }

    public void Dispose()
    {

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
