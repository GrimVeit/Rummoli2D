using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStartPlayersState_Game : IState
{
    private readonly IStateMachineProvider _stateProvider;
    private readonly List<IPlayer> _players;
    private readonly IPlayerPresentationProvider _playerPresentationProvider;

    private IEnumerator timer;

    public ShowStartPlayersState_Game(IStateMachineProvider stateProvider, List<IPlayer> players, IPlayerPresentationProvider playerPresentationProvider)
    {
        _stateProvider = stateProvider;
        _players = players;
        _playerPresentationProvider = playerPresentationProvider;
    }

    public void EnterState()
    {
        Debug.Log($"ACTIVATE STATE: <color=red>{this.GetType()}</color>");

        if(timer != null) Coroutines.Stop(timer);

        timer = Timer(0.2f, 0.3f);
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);
    }

    private IEnumerator Timer(float timeWaitOpen, float timeWaitChange)
    {
        for (int i = 0; i < _players.Count; i++)
        {
            _playerPresentationProvider.Show(_players[i].Id);

            yield return new WaitForSeconds(timeWaitOpen);
        }

        yield return new WaitForSeconds(timeWaitChange);

        ChangeStateToMovePlayersTableState();
    }

    private void ChangeStateToMovePlayersTableState()
    {
        _stateProvider.EnterState(_stateProvider.GetState<MovePlayersTableState_Game>());
    }
}
