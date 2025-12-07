using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorVisualPresenter : IDoorVisualActivatorProvider, IDoorVisualEventsProvider, IDoorVisualInfoProvider
{
    private readonly DoorVisualModel _model;
    private readonly DoorVisualView _view;

    public DoorVisualPresenter(DoorVisualModel model, DoorVisualView view)
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
        _view.OnChooseDoor += _model.ChooseDoor;

        _model.OnActivateInteraction += _view.ActivateInteraction;
        _model.OnDeactivateInteraction += _view.DeactivateInteraction;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseDoor -= _model.ChooseDoor;

        _model.OnActivateInteraction -= _view.ActivateInteraction;
        _model.OnDeactivateInteraction -= _view.DeactivateInteraction;
    }

    #region Output

    public event Action<Door> OnChooseDoor_Value
    {
        add => _model.OnChooseDoor_Value += value;
        remove => _model.OnChooseDoor_Value -= value;
    }

    public event Action<int> OnChooseDoor_Index
    {
        add => _model.OnChooseDoor_Index += value;
        remove => _model.OnChooseDoor_Index -= value;
    }

    public event Action OnChooseDoor
    {
        add => _model.OnChooseDoor += value;
        remove => _model.OnChooseDoor -= value;
    }

    #endregion

    #region Input

    public Door GetCurrentDoor() => _model.CurrentDoor;
    public int GetCurrentIndexDoor() => _model.CurrentDoorIndex;


    public void ActivateInteraction() => _model.ActivateInteraction();
    public void DeactivateInteraction() => _model.DeactivateInteraction();

    #endregion
}

public interface IDoorVisualActivatorProvider
{
    void ActivateInteraction();
    void DeactivateInteraction();
}

public interface IDoorVisualInfoProvider
{
    Door GetCurrentDoor();
    int GetCurrentIndexDoor();
}

public interface IDoorVisualEventsProvider
{
    public event Action<Door> OnChooseDoor_Value;
    public event Action<int> OnChooseDoor_Index;
    public event Action OnChooseDoor;
}
