using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLaurelModel
{
    public int ScoreLaurel => _scoreLaurel;

    private int _scoreLaurel;

    private readonly string KEY;

    public ScoreLaurelModel(string KEY)
    {
        this.KEY = KEY;
    }

    public void Initialize()
    {
        _scoreLaurel = PlayerPrefs.GetInt(KEY, 0);
        OnChangeCountLaurel?.Invoke(_scoreLaurel);
    }

    public void Dispose()
    {
        PlayerPrefs.SetInt(KEY, _scoreLaurel);
    }

    public void SetLaurel(int laurel)
    {
        _scoreLaurel = laurel;
        OnChangeCountLaurel?.Invoke(_scoreLaurel);
    }

    public void AddLaurel()
    {
        _scoreLaurel += 1;
        OnChangeCountLaurel?.Invoke(_scoreLaurel);
    }

    #region Output

    public event Action<int> OnChangeCountLaurel;

    #endregion
}
