using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawnerSystemPresenter : ICardSpawnerSystemEventsProvider, ICardSpawnerSystemProvider
{
    private readonly CardSpawnerSystemModel _model;
    private readonly CardSpawnerSystemView _view;

    public CardSpawnerSystemPresenter(CardSpawnerSystemModel model, CardSpawnerSystemView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        _view.OnSpawnCard += _model.SpawnEnd;

        _model.OnSpawnCard += _view.SpawnCard;
    }

    private void DeactivateEvents()
    {
        _view.OnSpawnCard -= _model.SpawnEnd;

        _model.OnSpawnCard -= _view.SpawnCard;
    }

    #region Output

    public event Action<int, ICard> OnSpawn
    {
        add => _model.OnSpawnCardEnd += value;
        remove => _model.OnSpawnCardEnd -= value;
    }

    #endregion

    #region Input

    public void Spawn(int playerId) => _model.Spawn(playerId);
    public void Reset() => _model.Reset();

    #endregion
}

public interface ICardSpawnerSystemProvider
{
    public void Spawn(int playerId);
    public void Reset();
}

public interface ICardSpawnerSystemEventsProvider
{
    public event Action<int, ICard> OnSpawn;
}
