using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPokerCard : MonoBehaviour
{
    [SerializeField] private Image imageSprite;

    public void SetData(Sprite spriteFace)
    {
        imageSprite.sprite = spriteFace;
    }
}
