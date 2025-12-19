using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorePlayerView : View, IIdentify
{
    public string GetID() => id;
    [SerializeField] private string id;

    [SerializeField] private TextMeshProUGUI textScore;

    public void SetScore(int score)
    {
        textScore.text = score.ToString();
    }
}
