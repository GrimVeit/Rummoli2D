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
        IScorePlayerProvider scorePlayerProvider,
        IPlayerPeopleCardVisualEventsProvider playerPeopleCardVisualEventsProvider,
        IPlayerPeopleCardVisualInteractiveActivatorProvider playerPeopleCardVisualInteractiveProvider,
        IPlayerPeopleCardVisualProvider playerPeopleCardVisualProvider,
        IPlayerPeopleSubmitEventsProvider playerPeopleSubmitEventsProvider,
        IPlayerPeopleSubmitActivatorProvider playerPeopleSubmitActivatorProvider)
    {
        var stateBet = new PlayerBetState_PlayerPeople(playerIndex, betSystemInteractiveActivatorProvider, scorePlayerProvider, betSystemProvider, betSystemInfoProvider, betSystemEventsProvider);
        stateBet.OnApplyBet += ApplyBet;
        states[typeof(PlayerBetState_PlayerPeople)] = stateBet;

        var state5Cards = new Choose5CardsState_PlayerPeople(playerPeopleCardVisualInteractiveProvider, playerPeopleCardVisualEventsProvider, playerPeopleCardVisualProvider, playerPeopleSubmitEventsProvider, playerPeopleSubmitActivatorProvider);
        state5Cards.OnChooseCards += Choose5Cards;
        states[typeof(Choose5CardsState_PlayerPeople)] = state5Cards;
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




    public event Action<List<ICard>> OnChoose5Cards;

    private void Choose5Cards(List<ICard> cards)
    {
        OnChoose5Cards?.Invoke(cards);
    }

    #endregion
}
