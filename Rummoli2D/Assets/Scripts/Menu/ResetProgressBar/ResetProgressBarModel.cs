using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProgressBarModel
{
    private readonly ISoundProvider _soundProvider;

    public ResetProgressBarModel(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void SetSliderValue(float value)
    {
        int intValue = Mathf.RoundToInt(value);

        if(intValue == 1)
        {
            _soundProvider.PlayOneShot("Click");

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
