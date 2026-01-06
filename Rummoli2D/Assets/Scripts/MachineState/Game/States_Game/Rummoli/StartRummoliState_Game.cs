using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRummoliState_Game : IState
{
    private readonly IStateMachineProvider _stateProvider;
    private readonly List<IPlayer> _players;
    private readonly IPlayerPresentationSystemProvider _playerPresentationProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly ICardBankPresentationSystemProvider _cardBankPresentationSystemProvider;

    private IEnumerator timer;

    public StartRummoliState_Game(IStateMachineProvider stateProvider, List<IPlayer> players, IPlayerPresentationSystemProvider playerPresentationProvider, UIGameRoot sceneRoot, ICardBankPresentationSystemProvider cardBankPresentationSystemProvider)
    {
        _stateProvider = stateProvider;
        _players = players;
        _playerPresentationProvider = playerPresentationProvider;
        _sceneRoot = sceneRoot;
        _cardBankPresentationSystemProvider = cardBankPresentationSystemProvider;
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

    private IEnumerator Timer(float timeWait)
    {
        int startIndex = Random.Range(0, _players.Count);

        for (int i = 0; i < _players.Count; i++)
        {
            int index = (startIndex + i) % _players.Count; // чтобы не выйти за границы

            if(index == 0)
            {
                _playerPresentationProvider.Show(_players[index].Id, () =>
                {
                    _playerPresentationProvider.MoveToLayout(_players[index].Id, "Game", () =>
                    {
                        _playerPresentationProvider.ShowCards(_players[index].Id);
                    });
                });
            }
            else
            {
                _playerPresentationProvider.Show(_players[index].Id, () =>
                {
                    _playerPresentationProvider.ShowCards(_players[index].Id);
                });
            }

            yield return new WaitForSeconds(timeWait);
        }

        _cardBankPresentationSystemProvider.Show(() => _cardBankPresentationSystemProvider.ShowBalance());

        yield return new WaitForSeconds(0.3f);

        ChangeStateToRummoli();
    }

    private void ChangeStateToRummoli()
    {
        _stateProvider.EnterState(_stateProvider.GetState<RummoliState_Game>());
    }
}
