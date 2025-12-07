using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScorePresenter
{
    private readonly PlayerScoreModel _model;

    public PlayerScorePresenter(PlayerScoreModel model)
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
