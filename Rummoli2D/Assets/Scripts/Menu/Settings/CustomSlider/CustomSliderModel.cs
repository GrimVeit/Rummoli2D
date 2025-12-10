using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CustomSliderModel
{
    private readonly ISoundProvider _soundProvider;

    public CustomSliderModel(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void Change()
    {
        //_soundProvider.PlayOneShot("VolumeChange");
    }

    public void SetValue(float value)
    {
        OnSetValue?.Invoke(value);
    }

    public void ChangeValue(float value)
    {
        OnChangedValue?.Invoke(value);
    }

    #region Output

    public event Action<float> OnSetValue;
    public event Action<float> OnChangedValue;

    #endregion
}
