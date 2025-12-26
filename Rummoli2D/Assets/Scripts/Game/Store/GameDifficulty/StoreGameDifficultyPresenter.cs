using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreGameDifficultyPresenter : IStoreGameDifficultyProvider, IStoreGameDifficultyInfoProvider, IStoreGameDifficultyListener
{
    private readonly StoreGameDifficultyModel _model;

    public StoreGameDifficultyPresenter(StoreGameDifficultyModel model)
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

    public event Action<GameDifficulty> OnGameDifficultyChanged
    {
        add => _model.OnGameDifficultyChanged += value;
        remove => _model.OnGameDifficultyChanged -= value;
    }

    #endregion


    #region Input

    public GameDifficulty GameDifficulty => _model.GameDifficulty;

    public void SetDifficulty(GameDifficulty diff) => _model.SetDifficulty(diff);

    #endregion
}

public interface IStoreGameDifficultyProvider
{
    public void SetDifficulty(GameDifficulty diff);
}

public interface IStoreGameDifficultyInfoProvider
{
    public GameDifficulty GameDifficulty { get; }
}

public interface IStoreGameDifficultyListener
{
    public event Action<GameDifficulty> OnGameDifficultyChanged;
}
