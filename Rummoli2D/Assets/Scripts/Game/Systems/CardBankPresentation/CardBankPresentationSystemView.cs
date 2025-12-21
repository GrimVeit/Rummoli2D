using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardBankPresentationSystemView : View
{
    [SerializeField] private List<CardBankPresentationTransform> cardBankPresentationTransforms = new();

    [SerializeField] private Transform transformCardBank;
    [SerializeField] private Transform transformBalance;

    [SerializeField] private float speedScaleCardBank;
    [SerializeField] private float speedMoveCardBank;
    [SerializeField] private float speedScaleBalance;

    private Tween tweenScaleCardBank;
    private Tween tweenMoveCardBank;
    private Tween tweenScaleBalance;

    public void Show(Action OnComplete)
    {
        tweenScaleCardBank?.Kill();

        transformCardBank.gameObject.SetActive(true);
        tweenScaleCardBank = transformCardBank.DOScale(1, speedScaleCardBank).OnComplete(() => OnComplete?.Invoke());
    }

    public void Hide(Action OnComplete)
    {
        tweenScaleCardBank?.Kill();

        tweenScaleCardBank = transformCardBank.DOScale(0, speedScaleCardBank).OnComplete(() =>
        {
            transformCardBank.gameObject.SetActive(false);
            OnComplete?.Invoke();
        });
    }

    public void MoveToLayout(string key, Action OnComplete = null)
    {
        tweenMoveCardBank?.Kill();

        var transformMove = GetTransform(key);

        if (transformMove == null)
        {
            Debug.LogWarning($"Not found TransformMove by CardBank with key - {key}");
            return;
        }

        tweenMoveCardBank = transformCardBank.DOLocalMove(transformMove.localPosition, speedMoveCardBank).OnComplete(() => OnComplete?.Invoke());
    }

    public void ShowBalance(Action OnComplete)
    {
        tweenScaleBalance?.Kill();

        tweenScaleBalance = transformBalance.DOScale(1, speedScaleBalance).OnComplete(() => OnComplete?.Invoke());
    }

    public void HideBalance(Action OnComplete)
    {
        tweenScaleBalance?.Kill();

        tweenScaleBalance = transformBalance.DOScale(0, speedScaleBalance).OnComplete(() => OnComplete?.Invoke());
    }

    private Transform GetTransform(string key)
    {
        return cardBankPresentationTransforms.Find(data => data.Key == key).TransformMove;
    }
}

[System.Serializable]
public class CardBankPresentationTransform
{
    [SerializeField] private string key;
    [SerializeField] private Transform transformMove;

    public string Key => key;
    public Transform TransformMove => transformMove;
}
