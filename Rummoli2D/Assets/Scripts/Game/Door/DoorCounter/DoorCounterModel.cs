using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCounterModel
{
    public int CountDoor => countDoor;

    private int countDoor = 0;

    public void Initialize()
    {
        Reset();
    }

    public void AddCount()
    {
        countDoor++;

        OnCountChanged?.Invoke(countDoor);
    }

    public void Reset()
    {
        countDoor = 0;

        OnCountChanged?.Invoke(countDoor);
    }

    #region Output

    public event Action<int> OnCountChanged;

    #endregion
}
