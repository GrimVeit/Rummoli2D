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
        IPlayerHighlightSystemProvider playerHighlightSystemProvider,
        IRoundPhasePresentationSystemProvider roundPhasePresentationSystemProvider,
        ICardBankPresentationSystemProvider cardBankPresentationSystemProvider,
        ICardSpawnerSystemEventsProvider cardSpawnerSystemEventsProvider,
        ICardSpawnerSystemProvider cardSpawnerSystemProvider,
        IPlayerPokerProvider playerPokerProvider,
        IPlayerPokerListener playerPokerListener,
        IBetSystemEventsProvider betSystemEventsProvider,
        IBetSystemProvider betSystemProvider,
        IStoreCardRummoliProvider storeCardRummoliProvider,
        ICardRummoliVisualActivator cardRummoliVisualActivator,
        IPlayerPopupEffectSystemProvider playerPopupEffectSystemProvider,
        ISectorConditionCheckerProvider sectorConditionCheckerProvider,
        IStoreRoundCurrentNumberInfoProvider storeRoundNumberInfoProvider,
        IStoreRoundCurrentNumberProvider storeRoundNumberProvider,
        ICounterPassPlayerSystemProvider counterPassPlayerSystemProvider,
        ICounterPassPlayerSystemActivatorProvider counterPassPlayerSystemActivatorProvider,
        IRummoliTablePresentationSystemProvider rummoliTablePresentationSystemProvider,
        IStoreRoundCountInfoProvider storeRoundCountInfoProvider)
    {
        states[typeof(StartState_Game)] = new StartState_Game(this, sceneRoot);
        states[typeof(ShowStartPlayersState_Game)] = new ShowStartPlayersState_Game(this, players, playerPresentationProvider);
        states[typeof(MovePlayersTableState_Game)] = new MovePlayersTableState_Game(this, players, playerPresentationProvider, rummoliTablePresentationSystemProvider);
        states[typeof(StartingBalanceState_Game)] = new StartingBalanceState_Game(this, players, playerPresentationProvider);

        states[typeof(RoundState_Game)] = new RoundState_Game(this, players, playerPresentationProvider, roundPhasePresentationSystemProvider, storeRoundNumberProvider, rummoliTablePresentationSystemProvider);
        states[typeof(Phase1State_Game)] = new Phase1State_Game(this, players, playerPresentationProvider, roundPhasePresentationSystemProvider, rummoliTablePresentationSystemProvider);
        states[typeof(BetState_Game)] = new BetState_Game(this, players);
        states[typeof(MovePlayerPeopleToGameState_Game)] = new MovePlayerPeopleToGameState_Game(this, players[0], playerPresentationProvider, cardBankPresentationSystemProvider, rummoliTablePresentationSystemProvider);
        states[typeof(DealCardsState_Game)] = new DealCardsState_Game(this, players, cardSpawnerSystemProvider, cardSpawnerSystemEventsProvider, cardBankPresentationSystemProvider);
        states[typeof(ChooseCardsPokerState_Game)] = new ChooseCardsPokerState_Game(this, players, playerPokerProvider, playerPresentationProvider);
        states[typeof(ResultPokerState_Game)] = new ResultPokerState_Game(this, players, playerPokerProvider, playerPresentationProvider, playerPokerListener, betSystemEventsProvider, betSystemProvider, cardBankPresentationSystemProvider, rummoliTablePresentationSystemProvider);
        states[typeof(Phase2State_Game)] = new Phase2State_Game(this, players, playerPresentationProvider, roundPhasePresentationSystemProvider);
        states[typeof(StartRummoliState_Game)] = new StartRummoliState_Game(this, players, playerPresentationProvider, cardBankPresentationSystemProvider, rummoliTablePresentationSystemProvider);
        states[typeof(RummoliState_Game)] = new RummoliState_Game(this, players, storeCardRummoliProvider, cardRummoliVisualActivator, playerHighlightSystemProvider, playerPopupEffectSystemProvider, sectorConditionCheckerProvider, playerPresentationProvider, betSystemProvider, betSystemEventsProvider, counterPassPlayerSystemProvider, counterPassPlayerSystemActivatorProvider, rummoliTablePresentationSystemProvider);
        states[typeof(RoundCompleteState_Game)] = new RoundCompleteState_Game(this, players, playerPresentationProvider, roundPhasePresentationSystemProvider, cardBankPresentationSystemProvider, rummoliTablePresentationSystemProvider);
        states[typeof(ReturnPlayersToStartState_Game)] = new ReturnPlayersToStartState_Game(this, players, playerPresentationProvider, storeRoundNumberInfoProvider, cardSpawnerSystemProvider, sectorConditionCheckerProvider, storeCardRummoliProvider, betSystemProvider, playerPokerProvider, rummoliTablePresentationSystemProvider, storeRoundCountInfoProvider);
    }

    public void Initialize()
    {
        EnterState(GetState<StartState_Game>());
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
