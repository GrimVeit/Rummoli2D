using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserGrid : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textNumber;
    [SerializeField] private TextMeshProUGUI textNickname;
    [SerializeField] private TextMeshProUGUI textRecord;
    [SerializeField] private Image imageAvatar;
    [SerializeField] private bool randomizeDots;

    public void SetData(int number, Sprite spriteAvatar, string nickname, int score)
    {
        textNumber.text = number.ToString();
        imageAvatar.sprite = spriteAvatar;
        textNickname.text = nickname;
        textRecord.text = $"{score}";

        FillWithDots(textNickname, nickname);
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
