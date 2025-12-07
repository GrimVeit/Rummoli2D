using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreAdditionallyPresenter : IStoreAdditionallyProvider, IStoreAdditionallyInfoProvider
{
    private readonly StoreAdditionallyModel _model;

    public StoreAdditionallyPresenter(StoreAdditionallyModel model)
    {
        _model = model;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _model.Dispose();
    }

    #region Input

    public void ActivateBonusCondition(int id) => _model.ActivateBonusCondition(id);
    public bool IsActiveBonusCondition(int id) => _model.IsActiveBonusCondition(id);

    #endregion
}

public interface IStoreAdditionallyProvider
{
    public void ActivateBonusCondition(int id);
}

public interface IStoreAdditionallyInfoProvider
{
    public bool IsActiveBonusCondition(int id);
}
