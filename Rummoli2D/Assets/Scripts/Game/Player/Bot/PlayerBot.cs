using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBot : IPlayer
{
    public int Id => _playerId;

    private readonly PlayerBotStateMachine _playerBotStateMachine;
    private readonly IHighlightProvider _highlightProvider;

    private readonly int _playerId;
    private readonly ScorePlayerPresenter _scorePlayerPresenter;

    public PlayerBot(
        int playerIndex,
        IHighlightProvider highlightProvider,
        BetSystemPresenter betSystemPresenter,
        ViewContainer viewContainer)
    {
        _playerId = playerIndex;
        _highlightProvider = highlightProvider;
        _scorePlayerPresenter = new ScorePlayerPresenter(new ScorePlayerModel(), viewContainer.GetView<ScorePlayerView>($"Bot_{_playerId}"));

        _playerBotStateMachine = new PlayerBotStateMachine
            (playerIndex,
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
        add => _playerBotStateMachine.OnApplyBet += value;
        remove => _playerBotStateMachine.OnApplyBet -= value;
    }

    #endregion

    #region Input

    public void SetScore(int score)
    {
        _scorePlayerPresenter.SetScore(score);
    }


    public void ActivateApplyBet()
    {
        _playerBotStateMachine.EnterState(_playerBotStateMachine.GetState<PlayerBetState_PlayerBot>());

        _highlightProvider.ActivateHighlight(_playerId);
    }

    public void DeactivateApplyBet()
    {
        _playerBotStateMachine.ExitState(_playerBotStateMachine.GetState<PlayerBetState_PlayerBot>());

        _highlightProvider.DeactivateHighlight(_playerId);
    }

    #endregion

    #endregion
}
