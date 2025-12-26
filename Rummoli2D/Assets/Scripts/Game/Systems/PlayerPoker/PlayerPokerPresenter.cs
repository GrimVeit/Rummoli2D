using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPokerPresenter : IPlayerPokerProvider, IPlayerPokerListener
{
    private readonly PlayerPokerModel _model;
    private readonly PlayerPokerView _view;

    public PlayerPokerPresenter(PlayerPokerModel model, PlayerPokerView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        _model.OnSetPlayer += _view.SetPlayer;
        _model.OnSetCount += _view.SetCountPlayer;
        _model.OnShowAll += _view.ShowAll;
        _model.OnSearchWinner += _view.ShowWin;
        _model.OnClearAll += _view.ClearAll;
    }

    private void DeactivateEvents()
    {
        _model.OnSetPlayer -= _view.SetPlayer;
        _model.OnSetCount -= _view.SetCountPlayer;
        _model.OnShowAll -= _view.ShowAll;
        _model.OnSearchWinner -= _view.ShowWin;
        _model.OnClearAll -= _view.ClearAll;
    }

    #region Output

    public event Action<int> OnSearchWinner
    {
        add => _model.OnSearchWinner += value;
        remove => _model.OnSearchWinner -= value;
    }

    #endregion

    #region Input

    public void SetCountPlayer(int count) => _model.SetCountPlayer(count);
    public void SetPlayer(int playerId, string name, List<ICard> cards) => _model.SetPlayer(playerId, name, cards);
    public void ClearAll() => _model.ClearAll();
    public void SearchWinner() => _model.SearchWinner();
    public void ShowAll() => _model.ShowAll();

    public void ShowTable() => _view.ShowTable();
    public void HideTable() => _view.HideTable();

    #endregion
}

public interface IPlayerPokerProvider
{
    void ShowTable();
    void HideTable();
    void SetCountPlayer(int count);
    void SetPlayer(int playerId, string name, List<ICard> cards);
    void ShowAll();
    void SearchWinner();
    void ClearAll();
}

public interface IPlayerPokerListener
{
    public event Action<int> OnSearchWinner;
}
