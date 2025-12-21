using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Face", menuName = "Game/Card/Theme/Face/New Face")]
public class CardFaceSO : ScriptableObject
{
    [SerializeField] private CardSuit _cardSuit;
    [SerializeField] private CardRank _cardRank;
    [SerializeField] private Sprite spriteFace;

    public CardSuit CardSuit => _cardSuit;
    public CardRank CardRank => _cardRank;
    public Sprite SpriteFace => spriteFace;
}
