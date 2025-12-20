using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoreCardPlayerModel
{
    public IReadOnlyList<CardData> Cards => cards;

    private readonly List<CardData> cards = new();

    public void AddCard(CardData card)
    {
        cards.Add(card);

        OnAddCard?.Invoke(card);
    }

    public void RemoveCard(CardData card)
    {
        cards.Remove(card);

        OnRemoveCard?.Invoke(card);
    }

    #region Output

    public event Action<CardData> OnAddCard;
    public event Action<CardData> OnRemoveCard;

    #endregion
}

public class CardData
{
    public CardSuit CardSuit => _cardSuit;
    public CardRank CardRank => _cardRank;

    private readonly CardSuit _cardSuit;
    private readonly CardRank _cardRank;

    public CardData(CardSuit cardSuit, CardRank cardRank)
    {
        _cardSuit = cardSuit;
        _cardRank = cardRank;
    }
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
