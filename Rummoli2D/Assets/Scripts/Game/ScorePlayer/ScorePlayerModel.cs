using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePlayerModel
{
    public int Score => _score;

    private int _score = 100;

    public void SetScore(int score)
    {
        _score = score;
        OnChangeScore?.Invoke(_score);
    }

    public void AddScore(int score)
    {
        _score += score;
        OnChangeScore?.Invoke(_score);
    }

    public void RemoveScore()
    {
        _score -= 1;
        OnChangeScore?.Invoke(_score);
    }

    #region Output

    public event Action<int> OnChangeScore;

    #endregion
}
