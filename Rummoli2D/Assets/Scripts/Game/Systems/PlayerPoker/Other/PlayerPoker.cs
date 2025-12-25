using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPoker : MonoBehaviour
{
    public PlayerPokerData PlayerPokerData => _data;

    [SerializeField] private TextMeshProUGUI textNickname;
    [SerializeField] private List<PlayerPokerCard> cards = new();

    private PlayerPokerData _data;

    public void SetData(PlayerPokerData data)
    {
        _data = data;

        textNickname.text = data.Name;

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].SetData(data.Cards[i].SpriteFace);
        }
    }
}
