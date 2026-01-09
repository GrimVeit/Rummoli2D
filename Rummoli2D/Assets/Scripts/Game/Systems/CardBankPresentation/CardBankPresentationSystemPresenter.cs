using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBankPresentationSystemPresenter : ICardBankPresentationSystemProvider
{
    private readonly CardBankPresentationSystemModel _model;
    private readonly CardBankPresentationSystemView _view;

    public CardBankPresentationSystemPresenter(CardBankPresentationSystemModel model, CardBankPresentationSystemView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnChooseDesign += _view.SetDesign;
    }

    private void DeactivateEvents()
    {
        _model.OnChooseDesign -= _view.SetDesign;
    }

    #region Input

    public void Show(Action OnComplete = null) => _view.Show(OnComplete);
    public void Hide(Action OnComplete = null) => _view.Hide(OnComplete);

    public void MoveToLayout(string key, Action OnComplete) => _view.MoveToLayout(key, OnComplete);

    public void ShowBalance(Action OnComplete = null) => _view.ShowBalance(OnComplete);
    public void HideBalance(Action OnComplete = null) => _view.HideBalance(OnComplete);

    #endregion
}

public interface ICardBankPresentationSystemProvider
{
    public void Show(Action OnComplete = null);
    public void Hide(Action OnComplete = null);

    void MoveToLayout(string key, Action OnComplete = null);

    void ShowBalance(Action OnComplete = null);
    void HideBalance(Action OnComplete = null);
}
