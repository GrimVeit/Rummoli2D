using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly List<IPlayer> _players;
    private readonly IPlayerPresentationSystemProvider _playerPresentationSystemProvider;
    private readonly IRoundPhasePresentationSystemProvider _roundPhasePresentationSystemProvider;
    private readonly UIGameRoot _sceneRoot;

    private IEnumerator timer;

    public RoundState_Game(IStateMachineProvider machineProvider, List<IPlayer> players, UIGameRoot sceneRoot, IPlayerPresentationSystemProvider presentationSystemProvider, IRoundPhasePresentationSystemProvider roundPhasePresentationSystemProvider)
    {
        _machineProvider = machineProvider;
        _players = players;
        _sceneRoot = sceneRoot;
        _playerPresentationSystemProvider = presentationSystemProvider;
        _roundPhasePresentationSystemProvider = roundPhasePresentationSystemProvider;
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
        _sceneRoot.CloseRummoliTablePanel();

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < _players.Count; i++)
        {
            _playerPresentationSystemProvider.Hide(_players[i].Id);

            yield return new WaitForSeconds(0.1f);
        }

        _roundPhasePresentationSystemProvider.ShowRound();

        yield return new WaitForSeconds(2f);

        _roundPhasePresentationSystemProvider.HideRound();

        ChangeStateToPhase1State();
    }

    private void ChangeStateToPhase1State()
    {
        _machineProvider.EnterState(_machineProvider.GetState<Phase1State_Game>());
    }
}
