using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDesignPresenter : IDoorDesignProvider
{
    private readonly DoorDesignModel _model;
    private readonly DoorDesignView _view;

    public DoorDesignPresenter(DoorDesignModel model, DoorDesignView view)
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
        _model.OnDesignChanged += _view.SetDesigns;
    }

    public void DeactivateEvents()
    {
        _model.OnDesignChanged -= _view.SetDesigns;
    }

    #region Input

    public void ShowOracle(int door) => _view.ShowOracle(door);
    public void ShowEvilTongue(int door) => _view.ShowEvilTongue(door);

    #endregion
}

public interface IDoorDesignProvider
{
    void ShowOracle(int door);
    void ShowEvilTongue(int door);
}
