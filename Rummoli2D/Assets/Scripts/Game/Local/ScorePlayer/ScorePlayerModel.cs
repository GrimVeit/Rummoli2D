using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePlayerModel
{
    public int TotalScore => _score;
    public int TotalEarnedScore => _totalEarnedScore;

    private int _score = 0;
    private int _totalEarnedScore = 0;

    public void SetScore(int score)
    {
        _score = score;
        OnAddScore?.Invoke(_score);
    }

    public void AddScore(int score)
    {
        _score += score;
        _totalEarnedScore += score;
        OnAddScore?.Invoke(_score);
    }

    public void RemoveScore()
    {
        _score -= 1;
        OnRemoveScore?.Invoke(_score);
    }

    #region Output

    public event Action<int> OnRemoveScore;
    public event Action<int> OnAddScore;

    #endregion
}
