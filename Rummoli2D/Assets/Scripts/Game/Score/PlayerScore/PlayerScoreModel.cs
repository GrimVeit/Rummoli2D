using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreModel
{
    private readonly IDoorCounterEventsProvider _doorCounterEventsProvider;
    private readonly IScoreRecordProvider _scoreRecordProvider;

    public PlayerScoreModel(IDoorCounterEventsProvider doorCounterEventsProvider, IScoreRecordProvider scoreRecordProvider)
    {
        _doorCounterEventsProvider = doorCounterEventsProvider;
        _scoreRecordProvider = scoreRecordProvider;
    }

    public void Initialize()
    {
        _doorCounterEventsProvider.OnCountChanged += SetDoorsCount;
    }

    public void Dispose()
    {
        _doorCounterEventsProvider.OnCountChanged -= SetDoorsCount;
    }

    private void SetDoorsCount(int count)
    {
        _scoreRecordProvider.SetRecordScore(count);
    }
}
