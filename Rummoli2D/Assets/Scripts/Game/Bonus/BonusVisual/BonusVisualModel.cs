using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusVisualModel
{
    private readonly IStoreBonusEventsProvider _storeBonusEventsProvider;
    private readonly IStoreBonusInfoProvider _storeBonusInfoProvider;
    private readonly ISoundProvider _soundProvider;

    public BonusVisualModel(IStoreBonusEventsProvider storeBonusEventsProvider, IStoreBonusInfoProvider storeBonusInfoProvider, ISoundProvider soundProvider)
    {
        _storeBonusEventsProvider = storeBonusEventsProvider;
        _storeBonusInfoProvider = storeBonusInfoProvider;
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        _storeBonusEventsProvider.OnChangedBonusCount += ChangeBonusCount;
    }

    public void Dispose()
    {
        _storeBonusEventsProvider.OnChangedBonusCount -= ChangeBonusCount;
    }


    public void ActivateInteraction()
    {
        OnActivateInteraction?.Invoke();
    }

    public void DeactivateInteraction()
    {
        OnDeactivateInteraction?.Invoke();
    }


    public void ChooseBonus(BonusType bonusType)
    {
        if(_storeBonusInfoProvider.BonusCount(bonusType) >= 1)
        {
            OnChooseBonus_Value?.Invoke(bonusType);
            OnChooseBonus?.Invoke();
            _soundProvider.PlayOneShot("Click");
        }
        else
        {
            //NOT HAVE BONUS
        }
    }

    #region Output

    public event Action<BonusType> OnChooseBonus_Value;
    public event Action OnChooseBonus;

    public event Action OnActivateInteraction;
    public event Action OnDeactivateInteraction;



    public event Action<BonusType, int> OnChangedBonusCount;

    private void ChangeBonusCount(BonusType type, int count)
    {
        OnChangedBonusCount?.Invoke(type, count);
    }

    #endregion
}
