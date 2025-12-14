using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDesignBuyVisualModel
{
    private readonly IStoreCardDesignEventsProvider _storeCardDesignEventsProvider;
    private readonly IStoreCardDesignProvider _storeCardDesignProvider;
    private readonly IStoreCardDesignInfoProvider _storeCardDesignInfoProvider;
    private readonly IMoneyProvider _moneyProvider;

    private int _currentDesignIndex = -1;
    private int _currentPrice = 0;

    public CardDesignBuyVisualModel(IStoreCardDesignEventsProvider storeCardDesignEventsProvider, IStoreCardDesignProvider storeCardDesignProvider, IStoreCardDesignInfoProvider storeCardDesignInfoProvider, IMoneyProvider moneyProvider)
    {
        _storeCardDesignEventsProvider = storeCardDesignEventsProvider;
        _storeCardDesignProvider = storeCardDesignProvider;
        _storeCardDesignInfoProvider = storeCardDesignInfoProvider;
        _moneyProvider = moneyProvider;

        _storeCardDesignEventsProvider.OnOpenDesign += Open;
        _storeCardDesignEventsProvider.OnCloseDesign += Close;
        _storeCardDesignEventsProvider.OnSelectDesign += Select;
        _storeCardDesignEventsProvider.OnDeselectDesign += Deselect;
    }

    public void Initialize()
    {
        ChooseDesign(_storeCardDesignInfoProvider.GetCardDesignIndex(), _currentPrice);
    }

    public void Dispose()
    {
        _storeCardDesignEventsProvider.OnOpenDesign -= Open;
        _storeCardDesignEventsProvider.OnCloseDesign -= Close;
        _storeCardDesignEventsProvider.OnSelectDesign -= Select;
        _storeCardDesignEventsProvider.OnDeselectDesign -= Deselect;
    }



    public void ChooseDesign(int id, int price)
    {
        if (_currentDesignIndex == id) return;

        OnUnchoose?.Invoke(_currentDesignIndex);

        _currentDesignIndex = id;
        _currentPrice = price;

        var dataDesign = _storeCardDesignInfoProvider.GetCardDesignData(_currentDesignIndex);

        if (dataDesign.IsOpen)
        {
            if (!dataDesign.IsSelect)
                _storeCardDesignProvider.SelectDesign(id);

            OnDeactivateBuy?.Invoke();
        }
        else
        {
            OnActivateBuy?.Invoke();
        }

        _currentDesignIndex = id;
        OnChoose?.Invoke(_currentDesignIndex);
    }

    public void Buy()
    {
        if (_moneyProvider.CanAfford(_currentPrice))
        {
            _storeCardDesignProvider.OpenDesign(_currentDesignIndex, () => _storeCardDesignProvider.SelectDesign(_currentDesignIndex));
            _moneyProvider.SendMoney(-_currentPrice);
        }
        else
        {
            Debug.Log("NOT MONEY FOR BUY");
        }
    }

    #region Output

    public event Action<int> OnOpen;
    public event Action<int> OnClose;
    public event Action<int> OnSelect;
    public event Action<int> OnDeselect;

    public event Action<int> OnChoose;
    public event Action<int> OnUnchoose;

    public event Action OnActivateBuy;
    public event Action OnDeactivateBuy;

    private void Open(int id)
    {
        OnOpen?.Invoke(id);
    }

    private void Close(int id)
    {
        OnClose?.Invoke(id);
    }

    private void Select(int id)
    {
        OnSelect?.Invoke(id);
    }

    private void Deselect(int id)
    {
        OnDeselect?.Invoke(id);
    }

    #endregion
}
