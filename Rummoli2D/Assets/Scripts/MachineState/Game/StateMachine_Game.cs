using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_Game : IStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_Game
        (List<IPlayer> players,
        UIGameRoot sceneRoot,
        IPlayerPresentationProvider playerPresentationProvider)
    {
        states[typeof(StartState_Game)] = new StartState_Game(this, sceneRoot);
        states[typeof(ShowStartPlayersState_Game)] = new ShowStartPlayersState_Game(this, players, playerPresentationProvider);
        states[typeof(MovePlayersTableState_Game)] = new MovePlayersTableState_Game(this, players, playerPresentationProvider, sceneRoot);
        states[typeof(StartingBalanceState_Game)] = new StartingBalanceState_Game(this, players, playerPresentationProvider);
        states[typeof(BetState_Game)] = new BetState_Game(players);
    }

    public void Initialize()
    {
        EnterState(GetState<StartState_Game>());
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
