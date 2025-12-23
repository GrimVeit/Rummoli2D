using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBotCardVisualModel
{
    private readonly IStoreCardEventsProvider _storeCardEventsProvider;

    public PlayerBotCardVisualModel(IStoreCardEventsProvider storeCardEventsProvider)
    {
        _storeCardEventsProvider = storeCardEventsProvider;

        _storeCardEventsProvider.OnAddCard += AddCard;
        _storeCardEventsProvider.OnRemoveCard += RemoveCard;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _storeCardEventsProvider.OnAddCard -= AddCard;
        _storeCardEventsProvider.OnRemoveCard -= RemoveCard;
    }

    private void AddCard(ICard card)
    {
        OnAddCard?.Invoke(card);
    }

    private void RemoveCard(ICard card)
    {
        OnRemoveCard?.Invoke(card);
    }

    #region Output

    public event Action<ICard> OnAddCard;
    public event Action<ICard> OnRemoveCard;

    #endregion
}
