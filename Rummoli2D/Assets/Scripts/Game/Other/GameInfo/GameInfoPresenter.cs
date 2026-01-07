using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfoPresenter : IGameInfoVisualActivater
{
    private readonly GameInfoModel _model;
    private readonly GameInfoView _view;

    public GameInfoPresenter(GameInfoModel model, GameInfoView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnSetDescription += _view.SetDescription;
        _model.OnSetDifficulty += _view.SetDifficulty;
    }

    private void DeactivateEvents()
    {
        _model.OnSetDescription -= _view.SetDescription;
        _model.OnSetDifficulty -= _view.SetDifficulty;
    }

    #region Input

    public void ActivateVisual() => _view.ActivateVisual();
    public void DeactivateVisual() => _view.DeactivateVisual();

    #endregion
}

public interface IGameInfoVisualActivater
{
    public void ActivateVisual();
    public void DeactivateVisual();
}

