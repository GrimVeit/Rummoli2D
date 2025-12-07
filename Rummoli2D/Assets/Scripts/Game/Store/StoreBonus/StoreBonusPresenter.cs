using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreBonusPresenter : IStoreBonusProvider, IStoreBonusEventsProvider, IStoreBonusInfoProvider
{
    private readonly StoreBonusModel _model;

    public StoreBonusPresenter(StoreBonusModel model)
    {
        _model = model;
    }

    public void Initialize()
    {
        _model.Initialize();
    }

    public void Dispose()
    {
        _model.Dispose();
    }

    #region Output

    public event Action<BonusType, int> OnChangedBonusCount
    {
        add => _model.OnChangedBonusCount += value;
        remove => _model.OnChangedBonusCount -= value;
    }

    #endregion

    #region Input

    public void AddBonus(BonusType bonusType, int count) => _model.AddBonus(bonusType, count);
    public void RemoveBonus(BonusType bonusType, int count) => _model.RemoveBonus(bonusType, count);
    public int BonusCount(BonusType bonusType) => _model.BonusCount(bonusType);

    #endregion
}

public interface IStoreBonusProvider
{
    public void AddBonus(BonusType bonusType, int count);
    public void RemoveBonus(BonusType bonusType, int count);
}

public interface IStoreBonusInfoProvider
{
    public int BonusCount(BonusType bonusType);
}

public interface IStoreBonusEventsProvider
{
    public event Action<BonusType, int> OnChangedBonusCount;
}
