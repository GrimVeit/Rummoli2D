using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerEarnScoreVisual : MonoBehaviour
{
    public int Score => _score;
    public int PlayerId => _playerId;

    [SerializeField] private TextMeshProUGUI textPlace;
    [SerializeField] private TextMeshProUGUI textNickname;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private bool randomizeDots;

    private int _playerId;
    private int _score;

    public void SetData(int playerId, string nickname)
    {
        _playerId = playerId;

        textNickname.text = nickname;

        FillWithDots(textNickname, nickname);
    }

    public void SetScore(int score)
    {
        _score = score;

        textScore.text = score.ToString();
    }

    public void SetPlace(int place)
    {
        textPlace.text = place.ToString();
    }

    string FillWithDots(TextMeshProUGUI tmp, string word)
    {
        string result = word + " ";
        int extraDots = 0;

        float targetWidth = tmp.rectTransform.rect.width;

        while (tmp.preferredWidth - 10 < targetWidth)
        {
            result += ".";
            tmp.text = result;
            extraDots++;

            if (randomizeDots && extraDots % 2 == 0 && tmp.preferredWidth - 10 + tmp.fontSize / 2 > targetWidth)
                break;
        }

        return result;
    }
}
