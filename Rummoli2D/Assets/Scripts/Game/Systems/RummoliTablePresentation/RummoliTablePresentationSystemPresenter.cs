using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RummoliTablePresentationSystemPresenter : IRummoliTablePresentationSystemProvider
{
    private readonly RummoliTablePresentationSystemView _view;

    public RummoliTablePresentationSystemPresenter(RummoliTablePresentationSystemView view)
    {
        _view = view;
    }

    #region Input

    public void Show(Action OnComplete = null) => _view.Show(OnComplete);
    public void Hide(Action OnComplete = null) => _view.Hide(OnComplete);

    public void MoveToLayout(string key, Action OnComplete = null) => _view.MoveToLayout(key, OnComplete);

    #endregion
}

public interface IRummoliTablePresentationSystemProvider
{
    public void Show(Action OnComplete = null);
    public void Hide(Action OnComplete = null);

    public void MoveToLayout(string key, Action OnComplete = null);
}
