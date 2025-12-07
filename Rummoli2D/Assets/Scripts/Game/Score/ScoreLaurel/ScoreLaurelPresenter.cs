using System;
using UnityEngine;

public class ScoreLaurelPresenter : IScoreLaurelEventsProvider, IScoreLaurelProvider, IScoreLaurelInfoProvider
{
    private readonly ScoreLaurelModel _model;

    public ScoreLaurelPresenter(ScoreLaurelModel model)
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

    public event Action<int> OnChangeCountLaurel
    {
        add => _model.OnChangeCountLaurel += value;
        remove => _model.OnChangeCountLaurel -= value;
    }

    #endregion

    #region Input

    public int ScoreLaurel() => _model.ScoreLaurel;
    public void SetLaurel(int laurel) => _model.SetLaurel(laurel);
    public void AddLaurel() => _model.AddLaurel();

    #endregion
}

public interface IScoreLaurelEventsProvider
{
    public event Action<int> OnChangeCountLaurel;
}

public interface IScoreLaurelProvider
{
    public void SetLaurel(int laurel);
    public void AddLaurel();
}

public interface IScoreLaurelInfoProvider
{
    public int ScoreLaurel();
}
