using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressScoreVisualModel
{
    private readonly IStoreProgressScoreInfoProvider _storeProgressScoreInfoProvider;
    private readonly IStoreProgressScoretListener _storeProgressScoretListener;

    public ProgressScoreVisualModel(IStoreProgressScoreInfoProvider storeProgressScoreInfoProvider, IStoreProgressScoretListener storeProgressScoretListener)
    {
        _storeProgressScoreInfoProvider = storeProgressScoreInfoProvider;
        _storeProgressScoretListener = storeProgressScoretListener;
    }

    public void Initialize()
    {
        ChangeScoreProgress(_storeProgressScoreInfoProvider.ScoreProgress);

        _storeProgressScoretListener.OnScoreProgressChanged += ChangeScoreProgress;
    }

    public void Dispose()
    {
        _storeProgressScoretListener.OnScoreProgressChanged -= ChangeScoreProgress;
    }

    #region Output

    public event Action<int> OnScoreProgressChanged;

    private void ChangeScoreProgress(int score)
    {
        OnScoreProgressChanged?.Invoke(score);
    }

    #endregion
}
