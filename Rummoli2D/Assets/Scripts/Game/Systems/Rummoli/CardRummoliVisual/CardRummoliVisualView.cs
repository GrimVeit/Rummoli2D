using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CardRummoliVisualView : View
{
    [SerializeField] private Transform transformRummoliVisual;
    [SerializeField] private Image imageRummoli;

    private Tween tweenScale;

    public void SetVisual(Sprite sprite)
    {
        imageRummoli.sprite = sprite;
    }

    public void ActivateVisual()
    {
        tweenScale?.Kill();

        tweenScale = transformRummoliVisual.DOScale(1, 0.3f);
    }

    public void DeactivateVisual()
    {
        tweenScale?.Kill();

        tweenScale = transformRummoliVisual.DOScale(0, 0.3f);
    }
}
