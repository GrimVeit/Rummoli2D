using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1State_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly List<IPlayer> _players;
    private readonly IPlayerPresentationSystemProvider _playerPresentationSystemProvider;
    private readonly IRoundPhasePresentationSystemProvider _roundPhasePresentationSystemProvider;
    private readonly IRummoliTablePresentationSystemProvider _rummoliTablePresentationSystemProvider;

    private IEnumerator timer;

    public Phase1State_Game(IStateMachineProvider machineProvider, List<IPlayer> players, IPlayerPresentationSystemProvider playerPresentationSystemProvider, IRoundPhasePresentationSystemProvider roundPhasePresentationSystemProvider, IRummoliTablePresentationSystemProvider rummoliTablePresentationSystemProvider)
    {
        _machineProvider = machineProvider;
        _players = players;
        _playerPresentationSystemProvider = playerPresentationSystemProvider;
        _roundPhasePresentationSystemProvider = roundPhasePresentationSystemProvider;
        _rummoliTablePresentationSystemProvider = rummoliTablePresentationSystemProvider;
    }

    public void EnterState()
    {
        if(timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);
    }

    private IEnumerator Timer()
    {
        _roundPhasePresentationSystemProvider.SetToLayoutNamePhase(0, "Preview");

        yield return new WaitForSeconds(0.5f);

        _roundPhasePresentationSystemProvider.ShowTextPhase(0);

        yield return new WaitForSeconds(0.3f);

        _roundPhasePresentationSystemProvider.ShowNamePhase(0);

        yield return new WaitForSeconds(2f);

        _roundPhasePresentationSystemProvider.HideTextPhase(0);
        _roundPhasePresentationSystemProvider.MoveToLayoutNamePhase(0, "Main");

        yield return new WaitForSeconds(0.3f);

        _rummoliTablePresentationSystemProvider.Show();

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < _players.Count; i++)
        {
            _playerPresentationSystemProvider.Show(_players[i].Id);

            yield return new WaitForSeconds(0.2f);
        }

        ChangeStateToBet();
    }

    private void ChangeStateToBet()
    {
        _machineProvider.EnterState(_machineProvider.GetState<BetState_Game>());
    }
}
