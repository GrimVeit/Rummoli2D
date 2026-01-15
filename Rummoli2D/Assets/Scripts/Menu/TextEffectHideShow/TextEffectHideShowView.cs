using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TextEffectHideShowView : View
{
    [SerializeField] private Material fontAsset;

    public void ActivateVisual(float duration)
    {
        fontAsset.SetFloat("_FaceDilate", -0.4f);

        float currentDilate = fontAsset.GetFloat("_FaceDilate");

        DOTween.To(
            () => currentDilate,
            x => { currentDilate = x; fontAsset.SetFloat("_FaceDilate", x); },
            0f,
            duration);
    }

    public void DeactivateVisual(float duration)
    {
        fontAsset.SetFloat("_FaceDilate", 0f);

        float currentDilate = fontAsset.GetFloat("_FaceDilate");

        DOTween.To(
            () => currentDilate,
            x => { currentDilate = x; fontAsset.SetFloat("_FaceDilate", x); },
            -0.4f,
            duration);
    }
}
