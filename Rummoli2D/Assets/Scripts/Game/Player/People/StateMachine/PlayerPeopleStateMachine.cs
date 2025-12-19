using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPeopleStateMachine : IStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    public PlayerPeopleStateMachine
        (int playerIndex,
        IBetSystemInteractiveActivatorProvider betSystemInteractiveActivatorProvider,
        IBetSystemEventsProvider betSystemEventsProvider,
        IBetSystemInfoProvider betSystemInfoProvider,
        IBetSystemProvider betSystemProvider,
        IScorePlayerProvider scorePlayerProvider)
    {
        var state = new PlayerBetState_PlayerPeople(playerIndex, betSystemInteractiveActivatorProvider, scorePlayerProvider, betSystemProvider, betSystemInfoProvider, betSystemEventsProvider);
        state.OnApplyBet += ApplyBet;
        states[typeof(PlayerBetState_PlayerPeople)] = state;


    }

    public IState GetState<T>() where T : IState
    {
        return states[typeof(T)];
    }

    public void EnterState(IState state)
    {
        state.EnterState();
    }

    public void ExitState(IState state)
    {
        state.ExitState();
    }

    #region Output

    public event Action OnApplyBet;
    private void ApplyBet()
    {
        OnApplyBet?.Invoke();
    }

    #endregion
}
