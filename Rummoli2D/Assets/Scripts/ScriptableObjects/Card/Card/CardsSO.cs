using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSOGroup", menuName = "Game/Card/New Group")]
public class CardsSO : ScriptableObject
{
    [SerializeField] private Theme themeDefault;

    public Theme ThemeDefault => themeDefault;
    public List<CardSO> Cards = new();
}
