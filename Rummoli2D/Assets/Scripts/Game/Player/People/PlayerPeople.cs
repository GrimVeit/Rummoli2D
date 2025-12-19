using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPeople : IPlayer
{
    public int Id => _playerId;

    private readonly PlayerPeopleStateMachine _playerPeopleStateMachine;
    private readonly IHighlightProvider _highlightProvider;

    private readonly int _playerId;
    private readonly ScorePlayerPresenter _scorePlayerPresenter;

    public PlayerPeople(
        int playerIndex, 
        IHighlightProvider highlightProvider,
        BetSystemPresenter betSystemPresenter,
        ViewContainer viewContainer)
    {
        _playerId = playerIndex;
        _highlightProvider = highlightProvider;
        _scorePlayerPresenter = new ScorePlayerPresenter(new ScorePlayerModel(), viewContainer.GetView<ScorePlayerView>("Player"));

        _playerPeopleStateMachine = new PlayerPeopleStateMachine
            (playerIndex, 
            betSystemPresenter, 
            betSystemPresenter,
            betSystemPresenter,
            betSystemPresenter,
            _scorePlayerPresenter);
    }

    public void Initialize()
    {
        _scorePlayerPresenter.Initialize();
    }

    public void Dispose()
    {
        _scorePlayerPresenter.Dispose();
    }

    #region API

    #region Output

    public event Action OnApplyBet
    {
        add => _playerPeopleStateMachine.OnApplyBet += value;
        remove => _playerPeopleStateMachine.OnApplyBet -= value;
    }

    #endregion

    #region Input

    public void SetScore(int score)
    {
        _scorePlayerPresenter.SetScore(score);
    }


    public void ActivateApplyBet()
    {
        _playerPeopleStateMachine.EnterState(_playerPeopleStateMachine.GetState<PlayerBetState_PlayerPeople>());

        _highlightProvider.ActivateHighlight(_playerId);
    }

    public void DeactivateApplyBet()
    {
        _playerPeopleStateMachine.ExitState(_playerPeopleStateMachine.GetState<PlayerBetState_PlayerPeople>());

        _highlightProvider.DeactivateHighlight(_playerId);
    }

    #endregion

    #endregion
}
