using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBotCardVisual : MonoBehaviour
{
    public ICard Card => _card;

    [SerializeField] private Image imageCover;
    private ICard _card;

    public void SetData(ICard card)
    {
        _card = card;

        imageCover.sprite = _card.SpriteCover;
    }
}
