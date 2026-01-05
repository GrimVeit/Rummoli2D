using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RummolyNameUtility
{
    public static string GetHandRankName(HandRank rank, Language language = Language.English)
    {
        return (rank, language) switch
        {
            (HandRank.HighCard, Language.English) => "High Card",
            (HandRank.HighCard, Language.Russia) => "Старшая карта",

            (HandRank.OnePair, Language.English) => "One Pair",
            (HandRank.OnePair, Language.Russia) => "Пара",

            (HandRank.TwoPair, Language.English) => "Two Pair",
            (HandRank.TwoPair, Language.Russia) => "Две пары",

            (HandRank.ThreeOfAKind, Language.English) => "Three of a Kind",
            (HandRank.ThreeOfAKind, Language.Russia) => "Тройка",

            (HandRank.Straight, Language.English) => "Straight",
            (HandRank.Straight, Language.Russia) => "Стрит",

            (HandRank.Flush, Language.English) => "Flush",
            (HandRank.Flush, Language.Russia) => "Флеш",

            (HandRank.FullHouse, Language.English) => "Full House",
            (HandRank.FullHouse, Language.Russia) => "Фулл-хаус",

            (HandRank.FourOfAKind, Language.English) => "Four of a Kind",
            (HandRank.FourOfAKind, Language.Russia) => "Каре",

            (HandRank.StraightFlush, Language.English) => "Straight Flush",
            (HandRank.StraightFlush, Language.Russia) => "Стрит-флеш",

            (HandRank.RoyalFlush, Language.English) => "Royal Flush",
            (HandRank.RoyalFlush, Language.Russia) => "Роял-флеш",

            _ => ""
        };
    }

    public static string GetRoundName_Open(int roundNumber, Language language = Language.English)
    {
        return language switch
        {
            Language.English => $"Round {roundNumber}",
            Language.Russia => $"Раунд {roundNumber}",
            _ => $"Round {roundNumber}"
        };
    }

    public static string GetRoundName_Complete(int roundNumber, Language language = Language.English)
    {
        return language switch
        {

            Language.English => $"Round <color=#F0BE61>{roundNumber}</color> completed",
            Language.Russia => $"Раунд <color=#F0BE61>{roundNumber}</color> завершён",
            _ => $"Round {roundNumber}"
        };
    }
}
