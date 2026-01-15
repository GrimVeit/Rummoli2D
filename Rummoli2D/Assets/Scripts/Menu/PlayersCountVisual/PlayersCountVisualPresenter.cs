using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersCountVisualPresenter
{
    private readonly PlayersCountVisualModel _model;
    private readonly PlayersCountVisualView _view;

    public PlayersCountVisualPresenter(PlayersCountVisualModel model, PlayersCountVisualView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnIncrease += _model.IncreaseCount;
        _view.OnDecrease += _model.DecreaseCount;

        _model.OnPlayersCountChanged += _view.SetCount;
    }

    private void DeactivateEvents()
    {
        _view.OnIncrease -= _model.IncreaseCount;
        _view.OnDecrease -= _model.DecreaseCount;

        _model.OnPlayersCountChanged -= _view.SetCount;
    }
}
