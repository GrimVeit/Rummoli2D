using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Face", menuName = "Game/Card/Theme/New Theme")]
public class CardThemeSO : ScriptableObject
{
    [SerializeField] private Theme themeCard;
    [SerializeField] private Sprite spriteCover;
    [SerializeField] private List<CardFaceSO> cardFaces = new();

    public Theme ThemeCard => themeCard;

    public Sprite GetSpriteCover()
    {
        return spriteCover;
    }

    public Sprite GetSpriteFace(CardSuit cardSuit, CardRank cardRank)
    {
        return cardFaces.Find(face => face.CardSuit == cardSuit && face.CardRank == cardRank).SpriteFace;
    }
}
