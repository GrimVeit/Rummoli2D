using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPeopleCardVisual : MonoBehaviour
{
    public ICard Card => _card;

    [SerializeField] private Image imageFace;
    private ICard _card;

    public void SetData(ICard card)
    {
        _card = card;

        imageFace.sprite = _card.SpriteFace;
    }
}
