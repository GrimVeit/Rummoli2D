using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreProgressScoreModel
{
    public int ScoreProgress => _scoreProgress;

    private readonly string _key;
    private int _scoreProgress;

    public StoreProgressScoreModel(string key)
    {
        _key = key;
    }

    public void Initialize()
    {
        _scoreProgress = PlayerPrefs.GetInt(_key, 0);

        OnScoreProgressChanged?.Invoke(_scoreProgress);
    }

    public void ResetScoreProgress()
    {
        _scoreProgress = 0;

        OnScoreProgressChanged?.Invoke(_scoreProgress);
    }

    public void AddScoreProgress(int score)
    {
        if(score < 0) return;

        _scoreProgress += score;

        OnScoreProgressChanged?.Invoke(_scoreProgress);
    }

    public void Dispose()
    {
        PlayerPrefs.SetInt(_key, _scoreProgress);
        PlayerPrefs.Save();
    }

    #region Output

    public event Action<int> OnScoreProgressChanged;

    #endregion
}
