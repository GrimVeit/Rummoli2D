using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBuyVisualModel
{
    private readonly IStoreBackgroundEventsProvider _storeBackgroundEventsProvider;
    private readonly IStoreBackgroundProvider _storeBackgroundProvider;
    private readonly IStoreBackgroundInfoProvider _storeBackgroundInfoProvider;
    private readonly IMoneyProvider _moneyProvider;

    private int _currentBackgroundIndex;
    private int _currentPrice = 0;

    public BackgroundBuyVisualModel(IStoreBackgroundEventsProvider storeBackgroundEventsProvider, IStoreBackgroundProvider storeBackgroundProvider, IStoreBackgroundInfoProvider storeBackgroundInfoProvider, IMoneyProvider moneyProvider)
    {
        _storeBackgroundEventsProvider = storeBackgroundEventsProvider;
        _storeBackgroundProvider = storeBackgroundProvider;
        _storeBackgroundInfoProvider = storeBackgroundInfoProvider;
        _moneyProvider = moneyProvider;

        _storeBackgroundEventsProvider.OnOpenBackground += Open;
        _storeBackgroundEventsProvider.OnCloseBackground += Close;
        _storeBackgroundEventsProvider.OnSelectBackground += Select;
        _storeBackgroundEventsProvider.OnDeselectBackground += Deselect;
    }

    public void Initialize()
    {
        _currentBackgroundIndex = _storeBackgroundInfoProvider.GetBackgroundIndex();
        ChooseBackground(_currentBackgroundIndex, _currentPrice);
    }

    public void Dispose()
    {
        _storeBackgroundEventsProvider.OnOpenBackground -= Open;
        _storeBackgroundEventsProvider.OnCloseBackground -= Close;
        _storeBackgroundEventsProvider.OnSelectBackground -= Select;
        _storeBackgroundEventsProvider.OnDeselectBackground -= Deselect;
    }



    public void ChooseBackground(int id, int price)
    {
        if(_currentBackgroundIndex == id) return;

        OnUnchoose?.Invoke(_currentBackgroundIndex);

        _currentBackgroundIndex = id;
        _currentPrice = price;

        var dataBackground = _storeBackgroundInfoProvider.GetBackgroundData(_currentBackgroundIndex);

        if (dataBackground.IsOpen)
        {
            if (!dataBackground.IsSelect)
                _storeBackgroundProvider.SelectBackground(id);

            OnDeactivateBuy?.Invoke();
        }
        else
        {
            OnActivateBuy?.Invoke();
        }

        _currentBackgroundIndex = id;
        OnChoose?.Invoke(_currentBackgroundIndex);
    }

    public void Buy()
    {
        if (_moneyProvider.CanAfford(_currentPrice))
        {
            _storeBackgroundProvider.OpenBackground(_currentBackgroundIndex, () => _storeBackgroundProvider.SelectBackground(_currentBackgroundIndex));
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
