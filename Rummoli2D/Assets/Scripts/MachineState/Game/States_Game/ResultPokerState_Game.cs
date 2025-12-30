using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPokerState_Game : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly IPlayerPokerProvider _playerPokerProvider;
    private readonly IPlayerPokerListener _playerPokerListener;
    private readonly IPlayerPresentationSystemProvider _playerPresentationSystemProvider;
    private readonly IBetSystemEventsProvider _betSystemEventsProvider;
    private readonly IBetSystemProvider _betSystemProvider;
    private readonly ICardBankPresentationSystemProvider _cardBankPresentationSystemProvider;
    private readonly UIGameRoot _sceneRoot;

    private readonly List<IPlayer> _players;
    private  int _winnerPlayerId = -1;

    private IEnumerator timer;

    public ResultPokerState_Game(IStateMachineProvider machineProvider, List<IPlayer> players, IPlayerPokerProvider playerPokerProvider, IPlayerPresentationSystemProvider playerPresentationSystemProvider, IPlayerPokerListener playerPokerListener, UIGameRoot sceneRoot, IBetSystemEventsProvider betSystemEventsProvider, IBetSystemProvider betSystemProvider, ICardBankPresentationSystemProvider cardBankPresentationSystemProvider)
    {
        _machineProvider = machineProvider;
        _players = players;
        _playerPokerProvider = playerPokerProvider;
        _playerPresentationSystemProvider = playerPresentationSystemProvider;
        _playerPokerListener = playerPokerListener;
        _sceneRoot = sceneRoot;
        _betSystemEventsProvider = betSystemEventsProvider;
        _betSystemProvider = betSystemProvider;
        _cardBankPresentationSystemProvider = cardBankPresentationSystemProvider;
    }

    public void EnterState()
    {
        _betSystemEventsProvider.OnReturnBet += ReturnBet;
        _playerPokerListener.OnSearchWinner += SetWinner;

        Debug.Log($"ACTIVATE STATE: <color=red>{this.GetType()}</color>");

        if(timer != null ) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        _betSystemEventsProvider.OnReturnBet -= ReturnBet;
        _playerPokerListener.OnSearchWinner -= SetWinner;

        if (timer != null) Coroutines.Stop(timer);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.2f);

        _cardBankPresentationSystemProvider.Hide();
        _playerPokerProvider.ShowTable();

        yield return new WaitForSeconds(1f);

        _playerPokerProvider.ShowAll();

        for (int i = 0; i < _players.Count; i++)
        {
            _playerPresentationSystemProvider.HideCards(_players[i].Id);
        }

        yield return new WaitForSeconds(7f);

        _playerPokerProvider.SearchWinner();

        yield return new WaitForSeconds(2f);

        _playerPokerProvider.ClearAll();

        yield return new WaitForSeconds(1);

        _playerPresentationSystemProvider.Show(_winnerPlayerId);

        yield return new WaitForSeconds(0.3f);

        _playerPresentationSystemProvider.MoveToLayout(_winnerPlayerId, "Table", () =>
        {
            _playerPresentationSystemProvider.ShowBalance(_winnerPlayerId);
        });
        _sceneRoot.OpenRummoliTablePanel();

        yield return new WaitForSeconds(0.7f);

        _betSystemProvider.StartReturnBet(_winnerPlayerId, 0);

        yield return new WaitForSeconds(1f);

        _playerPresentationSystemProvider.Hide(_winnerPlayerId);

        yield return new WaitForSeconds(0.5f);

        _sceneRoot.CloseRummoliTablePanel();

        ChangeStateToPhase2();
    }

    private void ReturnBet(int playerId, int score)
    {
        var player = GetPlayer(playerId);

        player.AddScore(score);
    }

    private void SetWinner(int playerId)
    {
        _winnerPlayerId = playerId;
    }

    private IPlayer GetPlayer(int playerId)
    {
        return _players.Find(player => player.Id == playerId);
    }

    private void ChangeStateToPhase2()
    {
        _machineProvider.EnterState(_machineProvider.GetState<Phase2State_Game>());
    }
}
