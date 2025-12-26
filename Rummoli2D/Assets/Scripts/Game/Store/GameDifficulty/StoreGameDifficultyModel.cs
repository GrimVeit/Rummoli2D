using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreGameDifficultyModel
{
    public GameDifficulty GameDifficulty => _currentDifficulty;

    private readonly string _key;
    private GameDifficulty _currentDifficulty;

    public StoreGameDifficultyModel(string key)
    {
        _key = key;
    }

    public void Initialize()
    {
        int value = PlayerPrefs.GetInt(_key, (int)GameDifficulty.Medium);

        if (!Enum.IsDefined(typeof(GameDifficulty), value))
            value = (int)GameDifficulty.Easy; // дефолтное значение

        _currentDifficulty = (GameDifficulty)value;
        OnGameDifficultyChanged?.Invoke(_currentDifficulty);
    }

    public void SetDifficulty(GameDifficulty diff)
    {
        _currentDifficulty = diff;
        OnGameDifficultyChanged?.Invoke(_currentDifficulty);
    }

    public void Dispose()
    {
        PlayerPrefs.SetInt(_key, (int)_currentDifficulty);
        PlayerPrefs.Save();
    }

    #region Output

    public event Action<GameDifficulty> OnGameDifficultyChanged;

    #endregion
}

public enum GameDifficulty
{
    Easy,
    Medium,
    Hard
}
