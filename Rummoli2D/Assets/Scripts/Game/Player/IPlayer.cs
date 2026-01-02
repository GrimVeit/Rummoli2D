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
    void AddCard(ICard card);
    void RemoveCard(ICard card);

    //Poker
    void ActiveChoose5Cards();
    void DeactivateChoose5Cards();
    event Action<IPlayer, List<ICard>> OnChoose5Cards;

    //Rummoli
    void ActivateRequestCard(CardData card);
    void DeactivateRequestCard(CardData card);

    void ActivateRequestRandomTwo();
    void DeactivateRequestRandomTwo();
    event Action<int, ICard> OnCardLaid;
    event Action<int> OnPass;

}


