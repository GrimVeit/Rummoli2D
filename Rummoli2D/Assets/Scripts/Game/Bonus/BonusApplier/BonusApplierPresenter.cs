using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusApplierPresenter : IBonusApplierProvider, IBonusApplierInfoProvider, IBonusApplierEventsProvider
{
    private readonly BonusApplierModel _model;

    public BonusApplierPresenter(BonusApplierModel model)
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

    public event Action OnNotApplyBonus
    {
        add => _model.OnNotApplyBonus += value;
        remove => _model.OnNotApplyBonus -= value;
    }

    #endregion

    #region Input

    public BonusType CurrentBonusType() => _model.CurrentBonusType;
    public void ApplyBonus() => _model.ApplyBonus();
    public void ApplyBonus(int doorId) => _model.ApplyBonus(doorId);

    #endregion
}

public interface IBonusApplierEventsProvider
{
    public event Action OnNotApplyBonus;
}

public interface IBonusApplierProvider
{
    void ApplyBonus();
    void ApplyBonus(int doorId);
}

public interface IBonusApplierInfoProvider
{
    public BonusType CurrentBonusType();
}
