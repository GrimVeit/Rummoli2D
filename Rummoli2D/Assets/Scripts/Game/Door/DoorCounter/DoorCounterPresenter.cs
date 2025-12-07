using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCounterPresenter : IDoorCounterInfoProvider, IDoorCounterEventsProvider, IDoorCounterProvider, IDisposable
{
    private readonly DoorCounterModel _model;
    private readonly DoorCounterView _view;

    public DoorCounterPresenter(DoorCounterModel model, DoorCounterView view)
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
    }

    private void ActivateEvents()
    {
        _model.OnCountChanged += _view.SetCount;

    }

    private void DeactivateEvents()
    {
        _model.OnCountChanged -= _view.SetCount;
    }

    #region Output

    public event Action<int> OnCountChanged
    {
        add => _model.OnCountChanged += value;
        remove => _model.OnCountChanged -= value;
    }

    #endregion

    #region Input
    
    public void Reset() => _model.Reset();
    public void AddCount() => _model.AddCount();

    public int GetCountDoor() => _model.CountDoor;

    #endregion
}

public interface IDoorCounterInfoProvider
{
    public int GetCountDoor();
}

public interface IDoorCounterEventsProvider
{
    public event Action<int> OnCountChanged;
}

public interface IDoorCounterProvider
{
    public void Reset();
    public void AddCount();
}
