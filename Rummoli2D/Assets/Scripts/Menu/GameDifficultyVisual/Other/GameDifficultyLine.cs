using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameDifficultyLine : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransformLine;
    [SerializeField] private float durationTime = 0.2f;

    private Tween tweenScale;

    public void Activate()
    {
        tweenScale?.Kill();

        tweenScale = rectTransformLine.DOScaleY(1, durationTime);
    }

    public void Deactivate()
    {
        tweenScale?.Kill();

        tweenScale = rectTransformLine.DOScaleY(0, durationTime);
    }
}
