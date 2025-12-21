using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICard
{
    public CardSuit CardSuit { get; }
    public CardRank CardRank { get; }
    public Sprite SpriteCover { get; }
    public Sprite SpriteFace { get; }
}
