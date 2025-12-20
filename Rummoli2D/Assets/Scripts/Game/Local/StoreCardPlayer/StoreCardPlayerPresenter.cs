using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreCardPlayerPresenter : IStoreCardInfoProvider, IStoreCardProvider, IStoreCardEventsProvider
{
    private readonly StoreCardPlayerModel _model;

    public StoreCardPlayerPresenter(StoreCardPlayerModel model)
    {
        _model = model;
    }

    public IReadOnlyList<CardData> Cards => _model.Cards;

    public void AddCard(CardData card) => _model.AddCard(card);

    public void RemoveCard(CardData card) => _model.RemoveCard(card);

    #region Output

    public event Action<CardData> OnAddCard
    {
        add => _model.OnAddCard += value;
        remove => _model.OnRemoveCard -= value;
    }

    public event Action<CardData> OnRemoveCard
    {
        add => _model.OnRemoveCard += value;
        remove => _model.OnRemoveCard -= value;
    }

    #endregion
}

public interface IStoreCardInfoProvider
{
    public IReadOnlyList<CardData> Cards { get; }
}

public interface IStoreCardProvider
{
    public void AddCard(CardData card);
    public void RemoveCard(CardData card);
}

public interface IStoreCardEventsProvider
{
    public event Action<CardData> OnAddCard;
    public event Action<CardData> OnRemoveCard;
}
