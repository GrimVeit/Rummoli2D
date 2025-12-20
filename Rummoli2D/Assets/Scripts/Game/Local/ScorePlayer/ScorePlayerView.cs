using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScorePlayerView : View, IIdentify
{
    public string GetID() => id;
    [SerializeField] private string id;

    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private Transform transformDisplay;

    private Vector3 defaultDisplayScale;
    private Tween tweenScale;

    public void Initialize()
    {
        defaultDisplayScale = transformDisplay.localScale;
    }

    public void Dispose()
    {

    }

    public void AddScore(int score)
    {
        tweenScale?.Kill();

        textScore.text = score.ToString();

        transformDisplay.localScale = defaultDisplayScale;

        tweenScale = transformDisplay.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.1f).OnComplete(() => tweenScale = transformDisplay.DOScale(defaultDisplayScale, 0.2f));
    }

    public void RemoveScore(int score)
    {
        tweenScale?.Kill();

        textScore.text = score.ToString();

        transformDisplay.localScale = defaultDisplayScale;

        tweenScale = transformDisplay.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).OnComplete(() => tweenScale = transformDisplay.DOScale(defaultDisplayScale, 0.2f));
    }
}
