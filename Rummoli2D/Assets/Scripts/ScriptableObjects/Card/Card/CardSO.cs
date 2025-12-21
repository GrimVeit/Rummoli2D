using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "Game/Card/New Card")]
public class CardSO : ScriptableObject
{
    [SerializeField] private CardSuit _cardSuit;
    [SerializeField] private CardRank _cardRank;

    public CardSuit CardSuit => _cardSuit;
    public CardRank CardRank => _cardRank;
}