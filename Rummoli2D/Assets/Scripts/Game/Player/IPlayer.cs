using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    int Id { get; }
    string Name { get; }

    //Balance
    void SetScore(int score);
    void AddScore(int score);

    //Bet
    void ActivateApplyBet();
    void DeactivateApplyBet();
    event Action OnApplyBet;

    //Cards
    int CardCount { get; }
    void AddCard(ICard card);
    void RemoveCard(ICard card);
    void DeleteCards();

    //Poker
    void ActiveChoose5Cards();
    void DeactivateChoose5Cards();
    event Action<IPlayer, List<ICard>> OnChoose5Cards;

    //Rummoli
    void ActivateRequestCard(CardData card);
    void DeactivateRequestCard();

    void ActivateRequestRandomTwo();
    void DeactivateRequestRandomTwo();
    event Action<int, ICard> OnCardLaid_Next;
    event Action<int> OnPass_Next;
    event Action<int, ICard> OnCardLaid_RandomTwo;
    event Action<int> OnPass_RandomTwo;

}


