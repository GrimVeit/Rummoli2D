using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardPokerHandSelectorModel
{
    private readonly IStoreGameDifficultyInfoProvider _gameDifficultyInfoProvider;

    public CardPokerHandSelectorModel(IStoreGameDifficultyInfoProvider gameDifficultyInfoProvider)
    {
        _gameDifficultyInfoProvider = gameDifficultyInfoProvider;
    }

    #region Hand
    public List<ICard> ChooseHandPlayer(List<ICard> cards)
    {
        return GetBestHand(cards);
    }

    public List<ICard> ChooseHandBot(List<ICard> cards)
    {
        var difficulty = _gameDifficultyInfoProvider.GameDifficulty;

        List<ICard> chosenHand = null;

        switch (difficulty)
        {
            case GameDifficulty.Easy:
                // —лаба€ рука: минимальные карты без повторов
                var sortedEasy = cards.OrderBy(c => c.CardRank).ToList();
                var distinctEasy = sortedEasy.GroupBy(c => c.CardRank).Select(g => g.First()).ToList();
                chosenHand = distinctEasy.Take(5).ToList();
                break;

            case GameDifficulty.Medium:
                // —редн€€ рука: пары/тройки
                var grouped = cards.GroupBy(c => c.CardRank)
                                   .OrderByDescending(g => g.Count())
                                   .ThenByDescending(g => g.Key)
                                   .ToList();
                var handMedium = new List<ICard>();

                foreach (var g in grouped)
                {
                    if (g.Count() >= 2 && handMedium.Count < 5)
                        handMedium.AddRange(g.Take(Math.Min(2, 5 - handMedium.Count)));
                }

                if (handMedium.Count < 5)
                {
                    foreach (var g in grouped)
                        foreach (var c in g)
                            if (handMedium.Count < 5 && !handMedium.Contains(c))
                                handMedium.Add(c);
                }

                chosenHand = handMedium;
                break;

            case GameDifficulty.Hard:
                // Pro-рука: максимально сильна€ комбинаци€
                chosenHand = GetBestHand(cards);
                break;

            default:
                chosenHand = cards.Take(5).ToList();
                break;
        }

        chosenHand.Shuffle();

        return chosenHand;
    }

    // -------------------------------
    // ¬нутренний метод: перебор всех 5-картных комбинаций дл€ максимальной руки
    // -------------------------------
    private List<ICard> GetBestHand(List<ICard> cards)
    {
        List<ICard> bestHand = null;
        HandRank bestRank = HandRank.HighCard;
        int n = cards.Count;

        for (int i = 0; i < n - 4; i++)
            for (int j = i + 1; j < n - 3; j++)
                for (int k = j + 1; k < n - 2; k++)
                    for (int l = k + 1; l < n - 1; l++)
                        for (int m = l + 1; m < n; m++)
                        {
                            var hand = new List<ICard> { cards[i], cards[j], cards[k], cards[l], cards[m] };
                            var rank = EvaluateHand(hand);
                            if (rank > bestRank)
                            {
                                bestRank = rank;
                                bestHand = hand;
                            }
                        }

        bestHand.Shuffle();

        return bestHand;
    }

    // -------------------------------
    // ¬нутренний метод: оценка комбинации из 5 карт
    // -------------------------------
    private static HandRank EvaluateHand(List<ICard> hand)
    {
        var ranks = hand.Select(c => (int)c.CardRank).OrderBy(r => r).ToList();
        var suits = hand.Select(c => c.CardSuit).ToList();
        bool isFlush = suits.Distinct().Count() == 1;
        bool isStraight = ranks.Distinct().Count() == 5 && ranks.Max() - ranks.Min() == 4;
        var groups = hand.GroupBy(c => c.CardRank).OrderByDescending(g => g.Count()).ToList();

        if (isStraight && isFlush && ranks.Max() == (int)CardRank.Ace) return HandRank.RoyalFlush;
        if (isStraight && isFlush) return HandRank.StraightFlush;
        if (groups[0].Count() == 4) return HandRank.FourOfAKind;
        if (groups[0].Count() == 3 && groups.Count > 1 && groups[1].Count() == 2) return HandRank.FullHouse;
        if (isFlush) return HandRank.Flush;
        if (isStraight) return HandRank.Straight;
        if (groups[0].Count() == 3) return HandRank.ThreeOfAKind;
        if (groups[0].Count() == 2 && groups.Count > 1 && groups[1].Count() == 2) return HandRank.TwoPair;
        if (groups[0].Count() == 2) return HandRank.OnePair;
        return HandRank.HighCard;
    }

    #endregion

    #region Winner
    public HandRank GetHandRank(List<ICard> hand)
    {
        var value = EvaluateHandValue(hand);
        return value.Rank;
    }

    public int GetWinner(Dictionary<int, List<ICard>> playersHands)
    {
        int winnerIndex = -1;
        HandValue bestHand = default;

        foreach (var kvp in playersHands)
        {
            int playerIndex = kvp.Key;
            List<ICard> hand = kvp.Value;

            HandValue value = EvaluateHandValue(hand);

            Debug.Log($"Player {playerIndex}: {value.Rank}");

            if (winnerIndex == -1 || value.CompareTo(bestHand) > 0)
            {
                bestHand = value;
                winnerIndex = playerIndex;
            }
        }

        return winnerIndex;
    }

    private static HandValue EvaluateHandValue(List<ICard> hand)
    {
        var ranks = hand.Select(c => (int)c.CardRank)
                        .OrderByDescending(r => r)
                        .ToList();

        var suits = hand.Select(c => c.CardSuit).ToList();

        var groups = hand.GroupBy(c => c.CardRank)
                         .OrderByDescending(g => g.Count())
                         .ThenByDescending(g => g.Key)
                         .ToList();

        bool isFlush = suits.Distinct().Count() == 1;
        bool isStraight = ranks.Distinct().Count() == 5 &&
                          ranks.Max() - ranks.Min() == 4;

        // Royal / Straight Flush
        if (isStraight && isFlush)
        {
            return new HandValue
            {
                Rank = ranks.Max() == (int)CardRank.Ace
                    ? HandRank.RoyalFlush
                    : HandRank.StraightFlush,
                TieBreakers = new List<int> { ranks.Max() }
            };
        }

        // Four of a Kind
        if (groups[0].Count() == 4)
        {
            return new HandValue
            {
                Rank = HandRank.FourOfAKind,
                TieBreakers = new List<int>
                {
                    (int)groups[0].Key,
                    (int)groups[1].Key
                }
            };
        }

        // Full House
        if (groups[0].Count() == 3 && groups[1].Count() == 2)
        {
            return new HandValue
            {
                Rank = HandRank.FullHouse,
                TieBreakers = new List<int>
                {
                    (int)groups[0].Key,
                    (int)groups[1].Key
                }
            };
        }

        // Flush
        if (isFlush)
        {
            return new HandValue
            {
                Rank = HandRank.Flush,
                TieBreakers = ranks
            };
        }

        // Straight
        if (isStraight)
        {
            return new HandValue
            {
                Rank = HandRank.Straight,
                TieBreakers = new List<int> { ranks.Max() }
            };
        }

        // Three of a Kind
        if (groups[0].Count() == 3)
        {
            return new HandValue
            {
                Rank = HandRank.ThreeOfAKind,
                TieBreakers = new List<int>
                {
                    (int)groups[0].Key
                }
                .Concat(groups.Skip(1).Select(g => (int)g.Key))
                .ToList()
            };
        }

        // Two Pair
        if (groups[0].Count() == 2 && groups[1].Count() == 2)
        {
            return new HandValue
            {
                Rank = HandRank.TwoPair,
                TieBreakers = new List<int>
                {
                    (int)groups[0].Key,
                    (int)groups[1].Key,
                    (int)groups[2].Key
                }
            };
        }

        // One Pair
        if (groups[0].Count() == 2)
        {
            return new HandValue
            {
                Rank = HandRank.OnePair,
                TieBreakers = new List<int>
                {
                    (int)groups[0].Key
                }
                .Concat(groups.Skip(1).Select(g => (int)g.Key))
                .ToList()
            };
        }

        // High Card
        return new HandValue
        {
            Rank = HandRank.HighCard,
            TieBreakers = ranks
        };
    }

    #endregion
}

public struct HandValue : IComparable<HandValue>
{
    public HandRank Rank;
    public List<int> TieBreakers;

    public readonly int CompareTo(HandValue other)
    {
        if (Rank != other.Rank)
            return Rank.CompareTo(other.Rank);

        for (int i = 0; i < TieBreakers.Count; i++)
        {
            if (TieBreakers[i] != other.TieBreakers[i])
                return TieBreakers[i].CompareTo(other.TieBreakers[i]);
        }

        return 0; // полна€ ничь€
    }
}

public enum HandRank
{
    HighCard = 1,
    OnePair = 2,
    TwoPair = 3,
    ThreeOfAKind = 4,
    Straight = 5,
    Flush = 6,
    FullHouse = 7,
    FourOfAKind = 8,
    StraightFlush = 9,
    RoyalFlush = 10
}
