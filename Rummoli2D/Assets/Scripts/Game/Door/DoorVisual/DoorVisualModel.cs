using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorVisualModel
{
    public int CurrentDoorIndex =>  _currentDoorIndex;
    public Door CurrentDoor => _currentDoor;

    private readonly IStoreDoorEventsProvider _storeDoorEventsProvider;

    private List<Door> _doors = new();

    private Door _currentDoor;
    private int _currentDoorIndex;

    public DoorVisualModel(IStoreDoorEventsProvider storeDoorEventsProvider)
    {
        _storeDoorEventsProvider = storeDoorEventsProvider;

        _storeDoorEventsProvider.OnDoorsCreated += SetDoors;
    }

    public void Initialize()
    {
        
    }

    public void Dispose()
    {
        _storeDoorEventsProvider.OnDoorsCreated -= SetDoors;
    }

    public void ActivateInteraction()
    {
        OnActivateInteraction?.Invoke();
    }

    public void DeactivateInteraction()
    {
        OnDeactivateInteraction?.Invoke();
    }

    public void ChooseDoor(int doorId)
    {
        _currentDoorIndex = doorId;
        _currentDoor = _doors[_currentDoorIndex];

        OnChooseDoor_Value?.Invoke(_currentDoor);
        OnChooseDoor_Index?.Invoke(_currentDoorIndex);
        OnChooseDoor?.Invoke();
    }

    private void SetDoors(List<Door> doors)
    {
        _doors = doors;
    }

    #region Output

    public event Action<Door> OnChooseDoor_Value;
    public event Action<int> OnChooseDoor_Index;
    public event Action OnChooseDoor;


    public event Action OnActivateInteraction;
    public event Action OnDeactivateInteraction;

    #endregion
}
