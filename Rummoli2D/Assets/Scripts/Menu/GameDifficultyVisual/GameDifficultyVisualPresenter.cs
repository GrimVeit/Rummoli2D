using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDifficultyVisualPresenter
{
    private readonly GameDifficultyVisualModel _model;
    private readonly GameDifficultyVisualView _view;

    public GameDifficultyVisualPresenter(GameDifficultyVisualModel model, GameDifficultyVisualView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _view.Initialize();
        _model.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _view.Dispose();
        _model.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnChooseGameDifficulty += _model.SetGameDifficulty;

        _model.OnSelectGameDifficulty += _view.SelectModul;
        _model.OnDeselectGameDifficulty += _view.DeselectModul;
        _model.OnChangeDescription += _view.SetText;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseGameDifficulty -= _model.SetGameDifficulty;

        _model.OnSelectGameDifficulty -= _view.SelectModul;
        _model.OnDeselectGameDifficulty -= _view.DeselectModul;
        _model.OnChangeDescription -= _view.SetText;
    }
}
