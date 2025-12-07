using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLaurelVisualModel
{
    private readonly IScoreLaurelInfoProvider _scoreLaurelInfoProvider;
    private readonly IScoreLaurelEventsProvider _scoreLaurelEventsProvider;

    public ScoreLaurelVisualModel(IScoreLaurelInfoProvider scoreLaurelInfoProvider, IScoreLaurelEventsProvider scoreLaurelEventsProvider)
    {
        _scoreLaurelInfoProvider = scoreLaurelInfoProvider;
        _scoreLaurelEventsProvider = scoreLaurelEventsProvider;

        _scoreLaurelEventsProvider.OnChangeCountLaurel += SetScoreLaurel;
    }

    public void Initialize()
    {
        OnScoreLaurelChanged?.Invoke(_scoreLaurelInfoProvider.ScoreLaurel());
    }

    public void Dispose()
    {
        _scoreLaurelEventsProvider.OnChangeCountLaurel -= SetScoreLaurel;
    }

    private void SetScoreLaurel(int laurel)
    {
        OnScoreLaurelChanged?.Invoke(laurel);
    }

    #region Output

    public event Action<int> OnScoreLaurelChanged;

    #endregion
}
