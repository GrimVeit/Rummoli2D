using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoreCardPlayerModel
{
    public IReadOnlyList<ICard> Cards => cards;

    private readonly List<ICard> cards = new();

    public void AddCard(ICard card)
    {
        cards.Add(card);

        OnAddCard?.Invoke(card);
    }

    public void RemoveCard(ICard card)
    {
        cards.Remove(card);

        OnRemoveCard?.Invoke(card);
    }

    public void DeleteCards()
    {
        cards.Clear();
        OnDeleteCards?.Invoke();
    }

    #region Output

    public event Action<ICard> OnAddCard;
    public event Action<ICard> OnRemoveCard;
    public event Action OnDeleteCards;

    #endregion
}

public enum CardSuit
{
    Clubs,      // ♣
    Diamonds,   // ♦
    Hearts,     // ♥
    Spades      // ♠
}

public enum CardRank
{
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13,
    Ace = 14
}
