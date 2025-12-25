using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPokerPresenter : IPlayerPokerProvider
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
        _model.OnClearAll += _view.ClearAll;
    }

    private void DeactivateEvents()
    {
        _model.OnSetPlayer -= _view.SetPlayer;
        _model.OnSetCount -= _view.SetCountPlayer;
        _model.OnClearAll -= _view.ClearAll;
    }

    #region Input

    public void SetCountPlayer(int count) => _model.SetCountPlayer(count);
    public void SetPlayer(int playerId, string name, List<ICard> cards) => _model.SetPlayer(playerId, name, cards);
    public void ClearAll() => _model.ClearAll();

    #endregion
}

public interface IPlayerPokerProvider
{
    void SetCountPlayer(int count);
    void SetPlayer(int playerId, string name, List<ICard> cards);
    void ClearAll();
}
