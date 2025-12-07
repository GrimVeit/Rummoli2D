using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusConditionPresenter
{
    private readonly BonusConditionModel _model;

    public BonusConditionPresenter(BonusConditionModel model)
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
}
