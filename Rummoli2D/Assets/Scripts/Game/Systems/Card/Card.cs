using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : ICard
{
    public CardSuit CardSuit { get; }
    public CardRank CardRank { get; }

    public Sprite SpriteCover { get; private set; }
    public Sprite SpriteFace { get; private set; }

    public Card(CardSO definition, Sprite spriteCover, Sprite spriteFace)
    {
        CardSuit = definition.CardSuit;
        CardRank = definition.CardRank;
        SpriteCover = spriteCover; 
        SpriteFace = spriteFace;
    }

    public void SetVisual(Sprite cover, Sprite face)
    {
        SpriteCover = cover;
        SpriteFace = face;
    }
}
