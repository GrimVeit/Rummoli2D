using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreDoorPresenter : IStoreDoorProvider, IStoreDoorEventsProvider
{
    private readonly StoreDoorModel _model;

    public StoreDoorPresenter(StoreDoorModel model)
    {
        _model = model;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    #region Output

    public event Action<List<Door>> OnDoorsCreated
    {
        add => _model.OnDoorsCreated += value;
        remove => _model.OnDoorsCreated -= value;
    }

    #endregion


    #region Input

    public void GenerateDoors() => _model.GenerateDoors();


    #endregion
}

public interface IStoreDoorProvider
{
    public void GenerateDoors();
}

public interface IStoreDoorEventsProvider
{
    public event Action<List<Door>> OnDoorsCreated;
}
