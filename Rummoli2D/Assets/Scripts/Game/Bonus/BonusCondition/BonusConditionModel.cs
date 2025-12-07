using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusConditionModel
{
    private readonly IStoreAdditionallyInfoProvider _storeAdditionallyInfoProvider;
    private readonly IDoorCounterEventsProvider _doorCounterEventsProvider;
    private readonly IStoreBonusProvider _doorBonusProvider;

    public BonusConditionModel(IStoreAdditionallyInfoProvider storeAdditionallyInfoProvider, IDoorCounterEventsProvider doorCounterEventsProvider, IStoreBonusProvider doorBonusProvider)
    {
        _storeAdditionallyInfoProvider = storeAdditionallyInfoProvider;
        _doorCounterEventsProvider = doorCounterEventsProvider;
        _doorBonusProvider = doorBonusProvider;

        _doorCounterEventsProvider.OnCountChanged += OnChangedDoorCount;
    }

    public void Initialize()
    {
        if (_storeAdditionallyInfoProvider.IsActiveBonusCondition(0))
        {
            _doorBonusProvider.AddBonus(BonusType.EvilTongue, 1);
        }

        if (_storeAdditionallyInfoProvider.IsActiveBonusCondition(2))
        {
            _doorBonusProvider.AddBonus(BonusType.Oracle, 1);
        }
    }

    public void Dispose()
    {
        _doorCounterEventsProvider.OnCountChanged -= OnChangedDoorCount;
    }

    private void OnChangedDoorCount(int count)
    {
        if(count == 0) return;

        if(count % 10 == 0 )
        {
            if (_storeAdditionallyInfoProvider.IsActiveBonusCondition(1))
            {
                _doorBonusProvider.AddBonus(BonusType.EvilTongue, 1);
            }

            if (_storeAdditionallyInfoProvider.IsActiveBonusCondition(3))
            {
                _doorBonusProvider.AddBonus(BonusType.Oracle, 1);
            }
        }
    }
}
