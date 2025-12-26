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
        ICardPokerSelectorBotProvider cardPokerSelectorBotProvider,
        IScorePlayerProvider scorePlayerProvider,
        IStoreCardInfoProvider storeCardInfoProvider)
    {
        var stateBet = new PlayerBetState_PlayerBot(playerIndex, scorePlayerProvider, betSystemProvider, betSystemInfoProvider, betSystemEventsProvider);
        stateBet.OnApplyBet += ApplyBet;
        states[typeof(PlayerBetState_PlayerBot)] = stateBet;

        var state5Cards = new Choose5CardsState_PlayerBot(storeCardInfoProvider, cardPokerSelectorBotProvider);
        state5Cards.OnChooseCards += Choose5Cards;
        states[typeof(Choose5CardsState_PlayerBot)] = state5Cards;
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
