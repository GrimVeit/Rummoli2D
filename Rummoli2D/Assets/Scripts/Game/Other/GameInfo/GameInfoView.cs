using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameInfoView : View
{
    [SerializeField] private TextMeshProUGUI textDifficulty;
    [SerializeField] private TextMeshProUGUI textRoundCount;
    [SerializeField] private TextMeshProUGUI textPlayersCount;
    [SerializeField] private TextMeshProUGUI textDescription;

    public void SetDifficulty(string text)
    {
        textDifficulty.text = text;
    }

    public void SetRoundCount(string text)
    {
        textRoundCount.text = text;
    }

    public void SetPlayersCount(string text)
    {
        textPlayersCount.text = text;
    }

    public void SetDescription(string text)
    {
        textDescription.text = text;
    }
}
