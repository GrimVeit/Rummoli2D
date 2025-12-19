using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingBalanceState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;

    private readonly List<IPlayer> _players;

    private IEnumerator timer;

    public StartingBalanceState_Game(IStateMachineProvider machineProvider, List<IPlayer> players)
    {
        _machineProvider = machineProvider;
        _players = players;
    }

    public void EnterState()
    {
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
        for (int i = 0; i < _players.Count; i++)
        {
            yield return new WaitForSeconds(time);

            _players[i].SetScore(100);
        }

        yield return new WaitForSeconds(time);

        ChangeToOtherState();
    }

    private void ChangeToOtherState()
    {
        _machineProvider.EnterState(_machineProvider.GetState<BetState_Game>());
    }
}
