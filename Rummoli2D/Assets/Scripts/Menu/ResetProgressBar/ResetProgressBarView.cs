using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetProgressBarView : View
{
    [SerializeField] private Slider sliderProgress;

    public void Initialize()
    {
        sliderProgress.onValueChanged.AddListener(SetValue);
    }

    public void Dispose()
    {
        sliderProgress.onValueChanged.RemoveListener(SetValue);
    }

    public void ResetSlider()
    {
        sliderProgress.value = 0;
    }

    private void SetValue(float value)
    {
        OnValueChanged?.Invoke(value);
    }

    #region Output

    public event Action<float> OnValueChanged;

    #endregion
}
