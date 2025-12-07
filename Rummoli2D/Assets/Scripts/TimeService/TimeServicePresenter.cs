using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeServicePresenter
{
    private readonly TimeServiceModel _model;

    public TimeServicePresenter(TimeServiceModel model)
    {
        _model = model;
    }

    public void Initialize()
    {
        _model.Initialize();
    }

    public void Dispose()
    {
        _model.Dispose();
    }

    #region Input

    public void CheckDateTime()
    {
        _model.CheckDateTime();
    }

    #endregion


    #region Output

    public event Action OnEventReached
    {
        add => _model.OnEventReached += value;
        remove => _model.OnEventReached -= value;
    }

    public event Action OnEventNotYet
    {
        add => _model.OnEventNotYet += value;
        remove => _model.OnEventNotYet -= value;
    }

    #endregion
}
