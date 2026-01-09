using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBankPresentationSystemModel
{
    private readonly IStoreCardDesignInfoProvider _cardDesignInfoProvider;

    public CardBankPresentationSystemModel(IStoreCardDesignInfoProvider cardDesignInfoProvider)
    {
        _cardDesignInfoProvider = cardDesignInfoProvider;
    }

    public void Initialize()
    {
        OnChooseDesign?.Invoke(_cardDesignInfoProvider.GetCardDesignIndex());
    }

    public void Dispose()
    {

    }

    #region Output

    public event Action<int> OnChooseDesign;

    #endregion
}
