using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBotStateMachine : IStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    public PlayerBotStateMachine
        (int playerIndex,
        IBetSystemEventsProvider betSystemEventsProvider,
        IBetSystemInfoProvider betSystemInfoProvider,
        IBetSystemProvider betSystemProvider,
        IScorePlayerProvider scorePlayerProvider)
    {
        var state = new PlayerBetState_PlayerBot(playerIndex, scorePlayerProvider, betSystemProvider, betSystemInfoProvider, betSystemEventsProvider);
        state.OnApplyBet += ApplyBet;
        states[typeof(PlayerBetState_PlayerBot)] = state;


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
