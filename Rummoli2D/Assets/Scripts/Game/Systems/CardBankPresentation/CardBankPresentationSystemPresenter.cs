using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBankPresentationSystemPresenter
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

    }

    public void Dispose()
    {

    }
}
