using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProgressBarModel
{
    public void SetSliderValue(float value)
    {
        int intValue = Mathf.RoundToInt(value);

        if(intValue == 1)
        {
            OnActivateReset?.Invoke();
        }
    }

    public void Reset()
    {
        OnReset?.Invoke();
    }

    #region Output

    public event Action OnReset;
    public event Action OnActivateReset;

    #endregion
}
