using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartingBalanceState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly IPlayerPresentationProvider _playerPresentationProvider;

    private readonly List<IPlayer> _players;

    private IEnumerator timer;

    public StartingBalanceState_Game(IStateMachineProvider machineProvider, List<IPlayer> players, IPlayerPresentationProvider playerPresentationProvider)
    {
        _machineProvider = machineProvider;
        _players = players;
        _playerPresentationProvider = playerPresentationProvider;
    }

    public void EnterState()
    {
        Debug.Log($"ACTIVATE STATE: <color=red>{this.GetType()}</color>");

        if (timer != null) Coroutines.Stop(timer);

        timer = Timer(0.3f);
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);
    }

    private IEnumerator Timer(float time)
    {
        int startIndex = Random.Range(0, _players.Count);

        for (int i = 0; i < _players.Count; i++)
        {
            int index = (startIndex + i) % _players.Count; // чтобы не выйти за границы

            _playerPresentationProvider.ShowBalance(_players[index].Id);

            yield return new WaitForSeconds(0.3f);

            _players[index].SetScore(100);

            yield return new WaitForSeconds(time);
        }

        yield return new WaitForSeconds(0.3f);

        ChangeToOtherState();
    }

    private void ChangeToOtherState()
    {
        _machineProvider.EnterState(_machineProvider.GetState<BetState_Game>());
    }
}
