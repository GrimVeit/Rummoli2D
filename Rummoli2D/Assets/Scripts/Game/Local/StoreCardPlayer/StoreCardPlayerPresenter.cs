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

    public IReadOnlyList<ICard> Cards => _model.Cards;

    public void AddCard(ICard card) => _model.AddCard(card);

    public void RemoveCard(ICard card) => _model.RemoveCard(card);

    #region Output

    public event Action<ICard> OnAddCard
    {
        add => _model.OnAddCard += value;
        remove => _model.OnRemoveCard -= value;
    }

    public event Action<ICard> OnRemoveCard
    {
        add => _model.OnRemoveCard += value;
        remove => _model.OnRemoveCard -= value;
    }

    #endregion
}

public interface IStoreCardInfoProvider
{
    public IReadOnlyList<ICard> Cards { get; }
}

public interface IStoreCardProvider
{
    public void AddCard(ICard card);
    public void RemoveCard(ICard card);
}

public interface IStoreCardEventsProvider
{
    public event Action<ICard> OnAddCard;
    public event Action<ICard> OnRemoveCard;
}
