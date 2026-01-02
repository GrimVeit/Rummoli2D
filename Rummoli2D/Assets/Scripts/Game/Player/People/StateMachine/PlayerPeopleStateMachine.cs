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
        IPlayerPeopleInputEventsProvider playerPeopleSubmitEventsProvider,
        IPlayerPeopleInputActivatorProvider playerPeopleSubmitActivatorProvider,
        ICardPokerSelectorPlayerProvider cardPokerSelectorPlayerProvider)
    {
        var stateBet = new PlayerBetState_PlayerPeople(playerIndex, betSystemInteractiveActivatorProvider, scorePlayerProvider, betSystemProvider, betSystemInfoProvider, betSystemEventsProvider);
        stateBet.OnApplyBet += ApplyBet;
        states[typeof(PlayerBetState_PlayerPeople)] = stateBet;

        var state5Cards = new Choose5CardsState_PlayerPeople(playerPeopleCardVisualInteractiveProvider, playerPeopleCardVisualEventsProvider, playerPeopleCardVisualProvider, playerPeopleSubmitEventsProvider, playerPeopleSubmitActivatorProvider, cardPokerSelectorPlayerProvider);
        state5Cards.OnChooseCards += Choose5Cards;
        states[typeof(Choose5CardsState_PlayerPeople)] = state5Cards;

        var stateRequestCard = new ChooseRequestCard_PlayerPeople(playerPeopleCardVisualInteractiveProvider, playerPeopleCardVisualEventsProvider, playerPeopleCardVisualProvider, playerPeopleSubmitEventsProvider, playerPeopleSubmitActivatorProvider);
        stateRequestCard.OnCardLaid += Choose_Next;
        stateRequestCard.OnPass += Pass_Next;
        states[typeof(ChooseRequestCard_PlayerPeople)] = stateRequestCard;

        var stateRequestCardRandomTwo = new ChooseRequestRandomTwo_PlayerPeople(playerPeopleCardVisualInteractiveProvider, playerPeopleCardVisualEventsProvider, playerPeopleCardVisualProvider, playerPeopleSubmitEventsProvider, playerPeopleSubmitActivatorProvider);
        stateRequestCardRandomTwo.OnCardLaid += Choose_RandomTwo;
        stateRequestCardRandomTwo.OnPass += Pass_RandomTwo;
        states[typeof(ChooseRequestRandomTwo_PlayerPeople)] = stateRequestCardRandomTwo;
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



    public event Action<ICard> OnCardLaid_Next;
    public event Action OnPass_Next;

    private void Choose_Next(ICard card)
    {
        OnCardLaid_Next?.Invoke(card);
    }

    private void Pass_Next()
    {
        OnPass_Next?.Invoke();
    }





    public event Action<ICard> OnCardLaid_RandomTwo;
    public event Action OnPass_RandomTwo;

    private void Choose_RandomTwo(ICard card)
    {
        OnCardLaid_RandomTwo?.Invoke(card);
    }

    private void Pass_RandomTwo()
    {
        OnPass_RandomTwo?.Invoke();
    }

    #endregion
}
