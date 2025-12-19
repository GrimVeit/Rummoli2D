using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBetState_PlayerBot : IState
{
    private readonly IScorePlayerProvider _scorePlayerProvider;
    private readonly IBetSystemProvider _betSystemProvider;
    private readonly IBetSystemInfoProvider _betSystemInfoProvider;
    private readonly IBetSystemEventsProvider _betSystemEventsProvider;

    private readonly int _playerIndex;

    public PlayerBetState_PlayerBot
        (int playerIndex,
        IScorePlayerProvider scorePlayerProvider,
        IBetSystemProvider betSystemProvider,
        IBetSystemInfoProvider betSystemInfoProvider,
        IBetSystemEventsProvider betSystemEventsProvider)
    {
        _playerIndex = playerIndex;
        _scorePlayerProvider = scorePlayerProvider;
        _betSystemProvider = betSystemProvider;
        _betSystemInfoProvider = betSystemInfoProvider;
        _betSystemEventsProvider = betSystemEventsProvider;
    }

    public void EnterState()
    {
        _betSystemEventsProvider.OnPlayerBetCompleted += ApplyBet;
        _betSystemEventsProvider.OnSubmitBet += SubmitBet;
        _betSystemEventsProvider.OnAddBet += AddBet;

        if (!_betSystemInfoProvider.IsPlayerBetCompleted(_playerIndex))
        {
            if (_betSystemInfoProvider.TryGetRandomAvailableSector(_playerIndex, out int index))
            {
                _betSystemProvider.AddBet(_playerIndex, index);
            }
        }
    }

    public void ExitState()
    {
        _betSystemEventsProvider.OnPlayerBetCompleted -= ApplyBet;
        _betSystemEventsProvider.OnSubmitBet -= SubmitBet;
        _betSystemEventsProvider.OnAddBet -= AddBet;
    }

    private void AddBet(int playerIndex, int sectorIndex)
    {
        if (_playerIndex == playerIndex)
        {
            _scorePlayerProvider.RemoveScore();
        }
    }

    private void SubmitBet(int playerIndex, int sectorIndex)
    {
        if (_playerIndex == playerIndex)
        {
            if (!_betSystemInfoProvider.IsPlayerBetCompleted(_playerIndex))
            {
                if (_betSystemInfoProvider.TryGetRandomAvailableSector(_playerIndex, out int index))
                {
                    _betSystemProvider.AddBet(_playerIndex, index);
                }
            }
        }

    }

    private void ApplyBet(int playerIndex)
    {
        if (_playerIndex == playerIndex)
            OnApplyBet?.Invoke();
    }

    #region Output

    public event Action OnApplyBet;

    #endregion
}
