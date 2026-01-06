using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CounterPassPlayerSystemView : View
{
    [SerializeField] private TextMeshProUGUI textPassCount;
    [SerializeField] private Transform transformPassCount;
    [SerializeField] private float timeVisual;

    private Tween tweenScale;

    public void SetPassName(string text)
    {
        textPassCount.text = text;
    }

    public void ActivateVisual(Action OnComplete)
    {
        tweenScale?.Kill();

        tweenScale = transformPassCount.DOScale(1, timeVisual).OnComplete(() => OnComplete?.Invoke());
    }

    public void DeactivateVisual(Action OnComplete)
    {
        tweenScale?.Kill();

        tweenScale = transformPassCount.DOScale(0, timeVisual).OnComplete(() => OnComplete?.Invoke());
    }
}
