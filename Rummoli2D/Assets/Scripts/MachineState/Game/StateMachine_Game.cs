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
        IPlayerPresentationSystemProvider playerPresentationProvider,
        ICardBankPresentationSystemProvider cardBankPresentationSystemProvider,
        ICardSpawnerSystemEventsProvider cardSpawnerSystemEventsProvider,
        ICardSpawnerSystemProvider cardSpawnerSystemProvider,
        IPlayerPokerProvider playerPokerProvider,
        IPlayerPokerListener playerPokerListener)
    {
        states[typeof(StartState_Game)] = new StartState_Game(this, sceneRoot);
        states[typeof(ShowStartPlayersState_Game)] = new ShowStartPlayersState_Game(this, players, playerPresentationProvider);
        states[typeof(MovePlayersTableState_Game)] = new MovePlayersTableState_Game(this, players, playerPresentationProvider, sceneRoot);
        states[typeof(StartingBalanceState_Game)] = new StartingBalanceState_Game(this, players, playerPresentationProvider);
        states[typeof(BetState_Game)] = new BetState_Game(this, players);
        states[typeof(MovePlayerPeopleToGameState_Game)] = new MovePlayerPeopleToGameState_Game(this, players[0], playerPresentationProvider, cardBankPresentationSystemProvider, sceneRoot);
        states[typeof(DealCardsState_Game)] = new DealCardsState_Game(this, players, cardSpawnerSystemProvider, cardSpawnerSystemEventsProvider, cardBankPresentationSystemProvider);
        states[typeof(ChooseCardsPokerState_Game)] = new ChooseCardsPokerState_Game(this, players, playerPokerProvider, playerPresentationProvider);
        states[typeof(ResultPokerState_Game)] = new ResultPokerState_Game(this, players, playerPokerProvider, playerPresentationProvider, playerPokerListener, sceneRoot);
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
