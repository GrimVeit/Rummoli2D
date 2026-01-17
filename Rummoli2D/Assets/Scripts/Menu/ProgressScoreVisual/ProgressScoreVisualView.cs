using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressScoreVisualView : View
{
    [SerializeField] private List<TextMeshProUGUI> textsProgress;
    [SerializeField] private List<TypeTextEffect> typeTextsEffect;

    public void SetScoreProgress(int score)
    {
        foreach (var text in textsProgress)
        {
            text.text = score.ToString();
        }

        foreach (var typeText in typeTextsEffect)
        {
            typeText.SetText(score.ToString());
        }
    }
}
