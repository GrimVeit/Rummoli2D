using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipCountVisualModel
{
    public event Action<int, int> OnChangeChipsCount;

    private readonly IStoreChipChangeEvents _storeChipChangeEvents;

    public ChipCountVisualModel(IStoreChipChangeEvents storeChipChangeEvents)
    {
        _storeChipChangeEvents = storeChipChangeEvents;

        _storeChipChangeEvents.OnChangeCountChips += ChangeChipsCount;
    }

    public void Initialize()
    {
        
    }

    public void Dispose()
    {
        _storeChipChangeEvents.OnChangeCountChips -= ChangeChipsCount;
    }

    public void ChangeChipsCount(int id, int count)
    {
        OnChangeChipsCount?.Invoke(id, count);
    }
}
