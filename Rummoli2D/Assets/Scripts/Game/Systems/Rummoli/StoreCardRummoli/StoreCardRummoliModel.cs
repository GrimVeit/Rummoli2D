using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoreCardRummoliModel
{
    public bool IsFinished { get; private set; }

    private readonly Dictionary<CardSuit, List<CardData>> _suits; // карты по мастям
    private readonly HashSet<CardSuit> _usedSuits;            // масти, полностью выложенные

    public CardData CurrentCardData; // текущая карта для выкладывания

    private readonly System.Random _random = new();

    public StoreCardRummoliModel()
    {
        _suits = new Dictionary<CardSuit, List<CardData>>();
        _usedSuits = new HashSet<CardSuit>();

        // создаём все карты
        foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
        {
            _suits[suit] = new List<CardData>();
            foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
            {
                _suits[suit].Add(new CardData(rank, suit));
            }

            _suits[suit] = _suits[suit].OrderBy(c => (int)c.Rank).ToList();
        }
    }

    public void Initialize()
    {
        IsFinished = false;

        CurrentCardData = _suits[CardSuit.Clubs].FirstOrDefault(c => c.Rank == CardRank.Two);
        OnCurrentCardDataChanged?.Invoke(CurrentCardData);
    }

    public CardData NextCard()
    {
        if (CurrentCardData == null) return null;

        var suitCards = _suits[CurrentCardData.Suit];
        int currentIndex = suitCards.FindIndex(c => c.Rank == CurrentCardData.Rank);

        if (currentIndex < 0 || currentIndex + 1 >= suitCards.Count)
        {
            _usedSuits.Add(CurrentCardData.Suit);
            CurrentCardData = null;
            OnCurrentCardDataChanged?.Invoke(CurrentCardData);
            CheckFinished();
            return null;
        }

        CurrentCardData = suitCards[currentIndex + 1];
        OnCurrentCardDataChanged?.Invoke(CurrentCardData);
        return CurrentCardData;
    }

    public CardData ChooseSuit(CardSuit suit)
    {
        if (_usedSuits.Contains(suit)) return null;

        var suitCards = _suits[suit];
        if (suitCards.Count < 2) return null;

        CurrentCardData = suitCards[1]; // тройка
        OnCurrentCardDataChanged?.Invoke(CurrentCardData);
        return CurrentCardData;
    }

    public CardData ChooseRandomSuit()
    {
        var remainingSuits = Enum.GetValues(typeof(CardSuit))
                                 .Cast<CardSuit>()
                                 .Where(s => !_usedSuits.Contains(s))
                                 .ToList();
        
        if (!remainingSuits.Any())
        {
            IsFinished = true;   // 👈 ФИНАЛ
            return null;
        }

        var suit = remainingSuits[_random.Next(remainingSuits.Count)];
        return ChooseSuit(suit);
    }

    public void Reset()
    {
        _suits.Clear();
        _usedSuits.Clear();
        IsFinished = false;

        foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
        {
            _suits[suit] = new List<CardData>();
            foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
            {
                _suits[suit].Add(new CardData(rank, suit));
            }

            _suits[suit] = _suits[suit].OrderBy(c => (int)c.Rank).ToList();
        }

        CurrentCardData = _suits[CardSuit.Clubs].FirstOrDefault(c => c.Rank == CardRank.Two);
    }

    private void CheckFinished()
    {
        if (_usedSuits.Count == Enum.GetValues(typeof(CardSuit)).Length)
        {
            IsFinished = true;
        }
    }


    #region Output

    public event Action<CardData> OnCurrentCardDataChanged;

    #endregion
}

public class CardData
{
    public CardSuit Suit { get; }
    public CardRank Rank { get; }

    public CardData(CardRank rank, CardSuit suit)
    {
        Suit = suit;
        Rank = rank;
    }
}
