using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayersTableState_Game : IState
{
    private readonly IStateMachineProvider _stateProvider;
    private readonly List<IPlayer> _players;
    private readonly IPlayerPresentationProvider _playerPresentationProvider;
    private readonly UIGameRoot _sceneRoot;

    private IEnumerator timer;

    public MovePlayersTableState_Game(IStateMachineProvider stateProvider, List<IPlayer> players, IPlayerPresentationProvider playerPresentationProvider, UIGameRoot sceneRoot)
    {
        _stateProvider = stateProvider;
        _players = players;
        _playerPresentationProvider = playerPresentationProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log($"ACTIVATE STATE: <color=red>{this.GetType()}</color>");

        if (timer != null) Coroutines.Stop(timer);

        timer = Timer(0.1f);
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);
    }

    private IEnumerator Timer(float timeWait)
    {
        int startIndex = Random.Range(0, _players.Count);

        for (int i = 0; i < _players.Count; i++)
        {
            int index = (startIndex + i) % _players.Count; // чтобы не выйти за границы
            _playerPresentationProvider.MoveToLayout(_players[index].Id, "Table");

            yield return new WaitForSeconds(timeWait);
        }

        _sceneRoot.OpenRummoliTablePanel();

        yield return new WaitForSeconds(0.3f);

        ChangeStatetToStartingBalanceState();
    }

    private void ChangeStatetToStartingBalanceState()
    {
        _stateProvider.EnterState(_stateProvider.GetState<StartingBalanceState_Game>());
    }
}
