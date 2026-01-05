using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundCompleteState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly List<IPlayer> _players;
    private readonly IPlayerPresentationSystemProvider _playerPresentationSystemProvider;
    private readonly IRoundPhasePresentationSystemProvider _roundPhasePresentationSystemProvider;
    private readonly UIGameRoot _sceneRoot;

    private IEnumerator timer;

    public RoundCompleteState_Game(IStateMachineProvider machineProvider, List<IPlayer> players, UIGameRoot sceneRoot, IPlayerPresentationSystemProvider presentationSystemProvider, IRoundPhasePresentationSystemProvider roundPhasePresentationSystemProvider)
    {
        _machineProvider = machineProvider;
        _players = players;
        _sceneRoot = sceneRoot;
        _playerPresentationSystemProvider = presentationSystemProvider;
        _roundPhasePresentationSystemProvider = roundPhasePresentationSystemProvider;
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
        _sceneRoot.CloseRummoliTablePanel();

        yield return new WaitForSeconds(0.1f);

        int startIndex = Random.Range(0, _players.Count);

        for (int i = 0; i < _players.Count; i++)
        {
            int index = (startIndex + i) % _players.Count; // чтобы не выйти за границы

            _playerPresentationSystemProvider.Hide(_players[index].Id);

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.1f);

        _roundPhasePresentationSystemProvider.HideNamePhase(1);

        yield return new WaitForSeconds(0.1f);

        _roundPhasePresentationSystemProvider.ShowRoundComplete();

        yield return new WaitForSeconds(2f);

        _roundPhasePresentationSystemProvider.HideRoundComplete();

        ChangeStateToReturnPlayerToStart();
    }

    private void ChangeStateToReturnPlayerToStart()
    {
        _machineProvider.EnterState(_machineProvider.GetState<ReturnPlayersToStartState_Game>());
    }
}
