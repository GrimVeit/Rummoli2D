using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorDesignModel
{
    private readonly IStoreDoorEventsProvider _storeDoorEventsProvider;

    public DoorDesignModel(IStoreDoorEventsProvider storeDoorEventsProvider)
    {
        _storeDoorEventsProvider = storeDoorEventsProvider;

        _storeDoorEventsProvider.OnDoorsCreated += ChangeDesign;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    #region Output

    public event Action<List<DoorType>> OnDesignChanged;

    private void ChangeDesign(List<Door> doors)
    {
        OnDesignChanged?.Invoke(doors.Select(data => data.Type).ToList());
    }

    #endregion
}
