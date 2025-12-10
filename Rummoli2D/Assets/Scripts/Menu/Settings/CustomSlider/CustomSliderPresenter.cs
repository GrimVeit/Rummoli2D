using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSliderPresenter : ICustomSliderProvider
{
    private readonly CustomSliderModel _model;
    private readonly CustomSliderView _view;

    public CustomSliderPresenter(CustomSliderModel model, CustomSliderView view)
    {
        _model = model; _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnChangeValue += _model.ChangeValue;
        _view.OnChange += _model.Change;

        _model.OnSetValue += _view.SetValue;
    }

    private void DeactivateEvents()
    {
        _view.OnChangeValue -= _model.ChangeValue;
        _view.OnChange -= _model.Change;

        _model.OnSetValue -= _view.SetValue;
    }

    #region Output

    public event Action<float> OnChangedValue
    {
        add => _model.OnChangedValue += value;
        remove => _model.OnChangedValue -= value;
    }

    public void SetValue(float value)
    {
        _model.SetValue(value);
    }

    #endregion
}

public interface ICustomSliderProvider
{
    public event Action<float> OnChangedValue;
    public void SetValue(float value);
}
