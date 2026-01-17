using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProgressBarPresenter : IResetProgressBarProvider, IResetProgressBarListener
{
    private readonly ResetProgressBarModel _model;
    private readonly ResetProgressBarView _view;

    public ResetProgressBarPresenter(ResetProgressBarModel model, ResetProgressBarView view)
    {
        _model = model;
        _view = view;
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
        _view.OnValueChanged += _model.SetSliderValue;

        _model.OnReset += _view.ResetSlider;
    }

    private void DeactivateEvents()
    {
        _view.OnValueChanged -= _model.SetSliderValue;

        _model.OnReset -= _view.ResetSlider;
    }

    #region Output

    public event Action OnActivateReset
    {
        add => _model.OnActivateReset += value;
        remove => _model.OnActivateReset -= value;
    }

    #endregion

    #region Input

    public void Reset() => _model.Reset();

    #endregion
}

public interface IResetProgressBarProvider
{
    public void Reset();
}

public interface IResetProgressBarListener
{
    public event Action OnActivateReset;
}
