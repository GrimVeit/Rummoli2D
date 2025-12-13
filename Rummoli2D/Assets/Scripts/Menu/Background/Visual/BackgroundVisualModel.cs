using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundVisualModel
{
    private readonly IStoreBackgroundEventsProvider _storeBackgroundEventsProvider;
    private readonly IStoreBackgroundInfoProvider _storeBackgroundInfoProvider;

    public BackgroundVisualModel(IStoreBackgroundEventsProvider storeBackgroundEventsProvider, IStoreBackgroundInfoProvider storeBackgroundInfoProvider)
    {
        _storeBackgroundEventsProvider = storeBackgroundEventsProvider;
        _storeBackgroundInfoProvider = storeBackgroundInfoProvider;
    }

    public void Initialize()
    {
        OnSelect?.Invoke(_storeBackgroundInfoProvider.GetBackgroundIndex(), false);

        _storeBackgroundEventsProvider.OnSelectBackground += Select;
    }

    public void Dispose()
    {
        _storeBackgroundEventsProvider.OnSelectBackground -= Select;
    }

    #region Input

    public event Action<int, bool> OnSelect;

    private void Select(int id)
    {
        OnSelect?.Invoke(id, true);
    }

    #endregion
}
