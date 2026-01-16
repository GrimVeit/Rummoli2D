using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllRoundsCompleteState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly List<IPlayer> _players;
    private readonly IPlayerPresentationSystemProvider _playerPresentationSystemProvider;
    private readonly IRoundPhasePresentationSystemProvider _roundPhasePresentationSystemProvider;
    private readonly IStoreRoundCurrentNumberProvider _storeRoundNumberProvider;
    private readonly IRummoliTablePresentationSystemProvider _rummoliTablePresentationSystemProvider;

    private IEnumerator timer;

    public AllRoundsCompleteState_Game(IStateMachineProvider machineProvider, List<IPlayer> players, IPlayerPresentationSystemProvider presentationSystemProvider, IRoundPhasePresentationSystemProvider roundPhasePresentationSystemProvider, IStoreRoundCurrentNumberProvider storeRoundNumberProvider, IRummoliTablePresentationSystemProvider rummoliTablePresentationSystemProvider)
    {
        _machineProvider = machineProvider;
        _players = players.Cast<IPlayer>().ToList();
        _playerPresentationSystemProvider = presentationSystemProvider;
        _roundPhasePresentationSystemProvider = roundPhasePresentationSystemProvider;
        _storeRoundNumberProvider = storeRoundNumberProvider;
        _rummoliTablePresentationSystemProvider = rummoliTablePresentationSystemProvider;
    }

    public void EnterState()
    {
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
        _rummoliTablePresentationSystemProvider.Hide();

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < _players.Count; i++)
        {
            _playerPresentationSystemProvider.Hide(_players[i].Id);

            yield return new WaitForSeconds(0.1f);
        }

        _roundPhasePresentationSystemProvider.ShowFinish();

        yield return new WaitForSeconds(2f);

        _roundPhasePresentationSystemProvider.HideFinish();

        ChangeStateToFinishState();
    }

    private void ChangeStateToFinishState()
    {
        _machineProvider.EnterState(_machineProvider.GetState<FinishState_Game>());
    }
}
