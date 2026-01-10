using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly List<IPlayerInfo> _players;
    private readonly IPlayerPresentationSystemProvider _playerPresentationSystemProvider;
    private readonly IRoundPhasePresentationSystemProvider _roundPhasePresentationSystemProvider;
    private readonly IStoreRoundCurrentNumberProvider _storeRoundNumberProvider;
    private readonly IRummoliTablePresentationSystemProvider _rummoliTablePresentationSystemProvider;

    private IEnumerator timer;

    public RoundState_Game(IStateMachineProvider machineProvider, List<IPlayer> players, IPlayerPresentationSystemProvider presentationSystemProvider, IRoundPhasePresentationSystemProvider roundPhasePresentationSystemProvider, IStoreRoundCurrentNumberProvider storeRoundNumberProvider, IRummoliTablePresentationSystemProvider rummoliTablePresentationSystemProvider)
    {
        _machineProvider = machineProvider;
        _players = players.Cast<IPlayerInfo>().ToList();
        _playerPresentationSystemProvider = presentationSystemProvider;
        _roundPhasePresentationSystemProvider = roundPhasePresentationSystemProvider;
        _storeRoundNumberProvider = storeRoundNumberProvider;
        _rummoliTablePresentationSystemProvider = rummoliTablePresentationSystemProvider;
    }

    public void EnterState()
    {
        if(timer != null) Coroutines.Stop(timer);

        _storeRoundNumberProvider.AddRound();

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

        _roundPhasePresentationSystemProvider.ShowRoundOpen();

        yield return new WaitForSeconds(2f);

        _roundPhasePresentationSystemProvider.HideRoundOpen();

        ChangeStateToPhase1State();
    }

    private void ChangeStateToPhase1State()
    {
        _machineProvider.EnterState(_machineProvider.GetState<Phase1State_Game>());
    }
}
