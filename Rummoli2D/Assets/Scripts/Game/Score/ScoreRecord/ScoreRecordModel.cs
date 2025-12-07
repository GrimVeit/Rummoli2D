using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecordModel
{
    public int ScoreRecord => _scoreRecord;

    private int _scoreRecord;

    private readonly string KEY;

    public ScoreRecordModel(string KEY)
    {
        this.KEY = KEY;
    }

    public void Initialize()
    {
        _scoreRecord = PlayerPrefs.GetInt(KEY, 0);
    }

    public void Dispose()
    {
        PlayerPrefs.SetInt(KEY, _scoreRecord);
    }

    public void SetScore(int score)
    {
        if(score > _scoreRecord)
        {
            _scoreRecord = score;
        }
    }
}
