using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresentationSystemPresenter : IPlayerPresentationSystemProvider
{
    private readonly PlayerPresentationSystemModel _model;
    private readonly PlayerPresentationSystemView _view;

    public PlayerPresentationSystemPresenter(PlayerPresentationSystemModel model, PlayerPresentationSystemView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    #region Input

    public void Show(int playerId, Action OnComplete = null) => _view.Show(playerId, OnComplete);
    public void Hide(int playerId, Action OnComplete = null) => _view.Hide(playerId, OnComplete);

    public void MoveToLayout(int playerId, string key, Action OnComplete) => _view.MoveToLayout(playerId, key, OnComplete);

    public void ShowBalance(int playerId, Action OnComplete = null) => _view.ShowBalance(playerId, OnComplete);
    public void HideBalance(int playerId, Action OnComplete = null) => _view.HideBalance(playerId, OnComplete);

    #endregion
}

public interface IPlayerPresentationSystemProvider
{
    public void Show(int playerId, Action OnComplete = null);
    public void Hide(int playerId, Action OnComplete = null);

    void MoveToLayout(int playerId, string key, Action OnComplete = null);

    void ShowBalance(int playerId, Action OnComplete = null);
    void HideBalance(int playerId, Action OnComplete = null);
}