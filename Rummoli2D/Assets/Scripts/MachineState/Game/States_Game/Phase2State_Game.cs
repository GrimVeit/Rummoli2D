using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2State_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly List<IPlayer> _players;
    private readonly IPlayerPresentationSystemProvider _playerPresentationSystemProvider;
    private readonly IRoundPhasePresentationSystemProvider _roundPhasePresentationSystemProvider;
    private readonly UIGameRoot _sceneRoot;

    private IEnumerator timer;

    public Phase2State_Game(IStateMachineProvider machineProvider, List<IPlayer> players, IPlayerPresentationSystemProvider playerPresentationSystemProvider, IRoundPhasePresentationSystemProvider roundPhasePresentationSystemProvider, UIGameRoot sceneRoot)
    {
        _machineProvider = machineProvider;
        _players = players;
        _playerPresentationSystemProvider = playerPresentationSystemProvider;
        _roundPhasePresentationSystemProvider = roundPhasePresentationSystemProvider;
        _sceneRoot = sceneRoot;
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
        _roundPhasePresentationSystemProvider.HideNamePhase(0);
        _roundPhasePresentationSystemProvider.SetToLayoutNamePhase(1, "Preview");

        yield return new WaitForSeconds(0.5f);

        _roundPhasePresentationSystemProvider.ShowTextPhase(1);

        yield return new WaitForSeconds(0.3f);

        _roundPhasePresentationSystemProvider.ShowNamePhase(1);

        yield return new WaitForSeconds(2f);

        _roundPhasePresentationSystemProvider.HideTextPhase(1);
        _roundPhasePresentationSystemProvider.MoveToLayoutNamePhase(1, "Main");
    }
}
