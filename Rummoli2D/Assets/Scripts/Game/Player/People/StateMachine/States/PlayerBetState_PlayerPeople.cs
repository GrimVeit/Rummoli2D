using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBetState_PlayerPeople : IState
{
    private readonly IBetSystemInteractiveActivatorProvider _betSystemInteractiveActivatorProvider;
    private readonly IScorePlayerProvider _scorePlayerProvider;
    private readonly IBetSystemProvider _betSystemProvider;
    private readonly IBetSystemInfoProvider _betSystemInfoProvider;
    private readonly IBetSystemEventsProvider _betSystemEventsProvider;
    private readonly UIGameRoot _sceneRoot;

    private readonly int _playerIndex;

    public PlayerBetState_PlayerPeople
        (int playerIndex,
        IBetSystemInteractiveActivatorProvider betSystemInteractiveActivatorProvider, 
        IScorePlayerProvider scorePlayerProvider,
        IBetSystemProvider betSystemProvider,
        IBetSystemInfoProvider betSystemInfoProvider,
        IBetSystemEventsProvider betSystemEventsProvider,
        UIGameRoot sceneRoot)
    {
        _playerIndex = playerIndex;
        _betSystemInteractiveActivatorProvider = betSystemInteractiveActivatorProvider;
        _scorePlayerProvider = scorePlayerProvider;
        _betSystemProvider = betSystemProvider;
        _betSystemInfoProvider = betSystemInfoProvider;
        _betSystemEventsProvider = betSystemEventsProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _betSystemEventsProvider.OnPlayerBetCompleted += ApplyBet;
        _betSystemEventsProvider.OnAddBet += SubmitBet;

        _betSystemInteractiveActivatorProvider.ActivateInteractive();

        _sceneRoot.OpenRightPanel();
    }

    public void ExitState()
    {
        _betSystemEventsProvider.OnPlayerBetCompleted -= ApplyBet;
        _betSystemEventsProvider.OnAddBet -= SubmitBet;

        _betSystemInteractiveActivatorProvider.DeactivateInteractive();
    }

    private void SubmitBet(int playerIndex, int sectorIndex)
    {
        if (_playerIndex == playerIndex)
            _scorePlayerProvider.RemoveScore();
    }

    private void ApplyBet(int playerIndex)
    {
        if (_playerIndex == playerIndex)
        {
            _sceneRoot.CloseRightPanel();
            OnApplyBet?.Invoke();
        }
    }

    #region Output

    public event Action OnApplyBet;

    #endregion
}
