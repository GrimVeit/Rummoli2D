using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStateModel
{
    public void OpenDoor(int id)
    {
        OnOpen?.Invoke(id);
    }

    public void ActivateAll()
    {
        OnActivateAll?.Invoke();
    }

    public void DeactivateAll()
    {
        OnDeactivateAll?.Invoke();
    }

    #region Output

    public event Action OnActivateAll;
    public event Action OnDeactivateAll;
    public event Action<int> OnOpen;

    #endregion
}
