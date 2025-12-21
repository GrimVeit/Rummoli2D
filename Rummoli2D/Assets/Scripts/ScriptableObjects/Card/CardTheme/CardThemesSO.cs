using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardThemeSOGroup", menuName = "Game/Card/Theme/New Group")]
public class CardThemesSO : ScriptableObject
{
    [SerializeField] private List<CardThemeSO> cardThemes = new();

    public CardThemeSO GetCardTheme(Theme theme)
    {
        return cardThemes.Find(data => data.ThemeCard == theme);
    }
}
