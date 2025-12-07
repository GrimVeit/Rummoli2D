using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoorVisualView : View
{
    [SerializeField] private List<DoorVisual> doorVisuals = new();

    public void Initialize()
    {
        doorVisuals.ForEach(data => data.OnChooseDoor += ChooseVisual);
    }

    public void Dispose()
    {
        doorVisuals.ForEach(data => data.OnChooseDoor -= ChooseVisual);
    }

    #region Input

    public void ActivateInteraction()
    {
        doorVisuals.ForEach(data => data.ActivateInteraction());
    }

    public void DeactivateInteraction()
    {
        doorVisuals.ForEach(data => data.DeactivateInteraction());
    }

    #endregion

    #region Output

    public event Action<int> OnChooseDoor;

    private void ChooseVisual(int doorId)
    {
        OnChooseDoor?.Invoke(doorId);
    }

    #endregion
}
