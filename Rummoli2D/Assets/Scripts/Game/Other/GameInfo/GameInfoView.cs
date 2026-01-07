using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameInfoView : View
{
    [SerializeField] private TextMeshProUGUI textDifficulty;
    [SerializeField] private TextMeshProUGUI textDescription;

    [SerializeField] private Material fontAsset;

    public void SetDifficulty(string text)
    {
        textDifficulty.text = text;
    }

    public void SetDescription(string text)
    {
        textDescription.text = text;
    }

    #region Visual
    public void ActivateVisual()
    {
        fontAsset.SetFloat("_FaceDilate", -0.4f);

        float currentDilate = fontAsset.GetFloat("_FaceDilate");

        DOTween.To(
            () => currentDilate,
            x => { currentDilate = x; fontAsset.SetFloat("_FaceDilate", x); },
            0f,
            1f);
    }

    public void DeactivateVisual()
    {
        fontAsset.SetFloat("_FaceDilate", 0f);

        float currentDilate = fontAsset.GetFloat("_FaceDilate");

        DOTween.To(
            () => currentDilate,
            x => { currentDilate = x; fontAsset.SetFloat("_FaceDilate", x); },
            -0.4f,
            1f);
    }
    #endregion
}
