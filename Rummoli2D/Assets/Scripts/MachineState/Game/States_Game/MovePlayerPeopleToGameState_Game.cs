using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerPeopleToGameState_Game : IState
{
    private readonly IStateMachineProvider _stateMachineProvider;
    private readonly IPlayer _playerPeople;
    private readonly IPlayerPresentationSystemProvider _playerPresentationProvider;
    private readonly ICardBankPresentationSystemProvider _cardBankPresentationSystemProvider;
    private readonly IRummoliTablePresentationSystemProvider _rummoliTablePresentationSystemProvider;

    private IEnumerator timer;

    public MovePlayerPeopleToGameState_Game(IStateMachineProvider stateMachineProvider, IPlayer playerPeople, IPlayerPresentationSystemProvider playerPresentationProvider, ICardBankPresentationSystemProvider cardBankPresentationSystemProvider, IRummoliTablePresentationSystemProvider rummoliTablePresentationSystemProvider)
    {
        _stateMachineProvider = stateMachineProvider;
        _playerPeople = playerPeople;
        _playerPresentationProvider = playerPresentationProvider;
        _cardBankPresentationSystemProvider = cardBankPresentationSystemProvider;
        _rummoliTablePresentationSystemProvider = rummoliTablePresentationSystemProvider;
    }

    public void EnterState()
    {
        Debug.Log($"ACTIVATE STATE: <color=red>{this.GetType()}</color>");
        if (timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);
    }

    private IEnumerator Timer()
    {
        _cardBankPresentationSystemProvider.MoveToLayout("Deal");
        _playerPresentationProvider.MoveToLayout(_playerPeople.Id, "Game");

        yield return new WaitForSeconds(0.1f);

        _rummoliTablePresentationSystemProvider.Hide();

        yield return new WaitForSeconds(0.1f);

        _cardBankPresentationSystemProvider.Show();

        yield return new WaitForSeconds(0.05f);

        ChangeStateToDealCards();
    }

    private void ChangeStateToDealCards()
    {
        _stateMachineProvider.EnterState(_stateMachineProvider.GetState<DealCardsState_Game>());
    }
}
