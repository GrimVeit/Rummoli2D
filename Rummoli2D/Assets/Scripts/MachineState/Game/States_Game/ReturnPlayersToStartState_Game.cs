using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPlayersToStartState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly List<IPlayer> _players;
    private readonly IPlayerPresentationSystemProvider _playerPresentationSystemProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IStoreRoundNumberInfoProvider _storeRoundNumberInfoProvider;
    private readonly IBetSystemProvider _betSystemProvider;
    private readonly ICardSpawnerSystemProvider _cardSpawnerSystemProvider;
    private readonly ISectorConditionCheckerProvider _sectorConditionCheckerProvider;
    private readonly IStoreCardRummoliProvider _storeCardRummoliProvider;
    private readonly IPlayerPokerProvider _playerPokerProvider;

    private IEnumerator timer;
    
    public ReturnPlayersToStartState_Game(IStateMachineProvider machineProvider, List<IPlayer> players, UIGameRoot sceneRoot, IPlayerPresentationSystemProvider presentationSystemProvider, IStoreRoundNumberInfoProvider storeRoundNumberInfoProvider, ICardSpawnerSystemProvider cardSpawnerSystemProvider, ISectorConditionCheckerProvider sectorConditionCheckerProvider, IStoreCardRummoliProvider storeCardRummoliProvider, IBetSystemProvider betSystemProvider, IPlayerPokerProvider playerPokerProvider)
    {
        _machineProvider = machineProvider;
        _players = players;
        _sceneRoot = sceneRoot;
        _betSystemProvider = betSystemProvider;
        _playerPresentationSystemProvider = presentationSystemProvider;
        _storeRoundNumberInfoProvider = storeRoundNumberInfoProvider;
        _cardSpawnerSystemProvider = cardSpawnerSystemProvider;
        _sectorConditionCheckerProvider = sectorConditionCheckerProvider;
        _storeCardRummoliProvider = storeCardRummoliProvider;
        _playerPokerProvider = playerPokerProvider;
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
        yield return new WaitForSeconds(0.1f);

        int startIndex = Random.Range(0, _players.Count);

        for (int i = 0; i < _players.Count; i++)
        {
            int index = (startIndex + i) % _players.Count; // чтобы не выйти за границы

            _playerPresentationSystemProvider.Show(_players[index].Id);

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.1f);

        _sceneRoot.OpenRummoliTablePanel();

        yield return new WaitForSeconds(0.1f);

        startIndex = Random.Range(0, _players.Count);

        for (int i = 0; i < _players.Count; i++)
        {
            int index = (startIndex + i) % _players.Count; // чтобы не выйти за границы

            if (_players[index].Id == 0)
            {
                if(_players[index].CardCount != 0)
                {
                    _playerPresentationSystemProvider.HideCards(_players[index].Id);
                    yield return new WaitForSeconds(0.2f);
                }

                _playerPresentationSystemProvider.MoveToLayout(0, "Table");

                yield return new WaitForSeconds(0.3f);
            }
            else
            {
                if (_players[index].CardCount != 0)
                {
                    _playerPresentationSystemProvider.HideCards(_players[index].Id);

                    yield return new WaitForSeconds(0.2f);
                }
            }

            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < _players.Count; i++)
        {
            _players[i].DeleteCards();
            _playerPresentationSystemProvider.ShowCards(_players[i].Id);
        }

        _playerPokerProvider.HideTable();
        _betSystemProvider.Reset();
        _cardSpawnerSystemProvider.Reset();
        _sectorConditionCheckerProvider.Reset();
        _storeCardRummoliProvider.Reset();

        yield return new WaitForSeconds(0.2f);

        if(_storeRoundNumberInfoProvider.RoundNumber == 10)
        {
            ChangeStateToFinish();
        }
        else
        {
            ChangeStateToRoundState();
        }
    }

    private void ChangeStateToFinish()
    {
        Debug.Log("FINISH");
    }

    private void ChangeStateToRoundState()
    {
        _machineProvider.EnterState(_machineProvider.GetState<RoundState_Game>());
    }
}
