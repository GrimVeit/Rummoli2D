using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInfo
{
    int Id { get; }
    string Name { get; }
}

public interface IPlayerScore
{
    int TotalScore { get; }
    int TotalEarnedScore { get; }
    event Action<int, int> OnAddScore;
    event Action<int, int> OnRemoveScore;
    void SetScore(int score);
    void AddScore(int score);
}

public interface IPlayerBet
{
    void ActivateApplyBet();
    void DeactivateApplyBet();
    event Action OnApplyBet;
}

public interface IPlayerCards
{
    int CardCount { get; }
    void AddCard(ICard card);
    void RemoveCard(ICard card);
    void DeleteCards();
}

public interface IPlayerPoker
{
    void ActiveChoose5Cards();
    void DeactivateChoose5Cards();
    event Action<IPlayer, List<ICard>> OnChoose5Cards;
}

public interface IPlayerRummoli
{
    void ActivateRequestCard(CardData card);
    void DeactivateRequestCard();
    void ActivateRequestRandomTwo();
    void DeactivateRequestRandomTwo();
    event Action<int, ICard> OnCardLaid_Next;
    event Action<int> OnPass_Next;
    event Action<int, ICard> OnCardLaid_RandomTwo;
    event Action<int> OnPass_RandomTwo;
}

public interface IPlayerMoney
{
    void SendMoney(int count);
    void SendProgressScore(int count);
}

// Составной интерфейс, "для чистоты"
public interface IPlayer :
    IPlayerInfo,
    IPlayerScore,
    IPlayerBet,
    IPlayerCards,
    IPlayerPoker,
    IPlayerRummoli,
    IPlayerMoney
{
}


