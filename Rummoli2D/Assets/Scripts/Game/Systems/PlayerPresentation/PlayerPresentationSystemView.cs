using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerPresentationSystemView : View
{
    [SerializeField] private List<PlayerPresentation> playerPresentations = new();

    [SerializeField] private float speedScalePlayer;
    [SerializeField] private float speedMovePlayer;

    [SerializeField] private float speedMoveAvatar;

    [SerializeField] private float speedScaleBalance;

    [SerializeField] private float speedScaleCards;

    public void Show(int playerId, Action OnComplete)
    {
        var playerPresentation = GetPlayerPresentation(playerId);

        if (playerPresentation == null)
        {
            Debug.LogWarning("Not found PlayerPresentation with PlayerId - " + playerId);
            return;
        }

        playerPresentation.Show(speedScalePlayer, OnComplete);
    }

    public void Hide(int playerId, Action OnComplete)
    {
        var playerPresentation = GetPlayerPresentation(playerId);

        if (playerPresentation == null)
        {
            Debug.LogWarning("Not found PlayerPresentation with PlayerId - " + playerId);
            return;
        }

        playerPresentation.Hide(speedScalePlayer, OnComplete);
    }

    public void MoveToLayout(int playerId, string key, Action OnComplete)
    {
        var playerPresentation = GetPlayerPresentation(playerId);

        if(playerPresentation == null)
        {
            Debug.LogWarning("Not found PlayerPresentation with PlayerId - " + playerId);
            return;
        }

        playerPresentation.MoveToLayout(key, speedMovePlayer, OnComplete);
    }

    public void ShowBalance(int playerId, Action OnComplete)
    {
        var playerPresentation = GetPlayerPresentation(playerId);

        if (playerPresentation == null)
        {
            Debug.LogWarning("Not found PlayerPresentation with PlayerId - " + playerId);
            return;
        }

        playerPresentation.ShowBalance(speedMoveAvatar, speedScaleBalance, OnComplete);
    }


    public void HideBalance(int playerId, Action OnComplete)
    {
        var playerPresentation = GetPlayerPresentation(playerId);

        if (playerPresentation == null)
        {
            Debug.LogWarning("Not found PlayerPresentation with PlayerId - " + playerId);
            return;
        }

        playerPresentation.HideBalance(speedMoveAvatar, speedScaleBalance, OnComplete);
    }


    public void ShowCards(int playerId, Action OnComplete)
    {
        var playerPresentation = GetPlayerPresentation(playerId);

        if (playerPresentation == null)
        {
            Debug.LogWarning("Not found PlayerPresentation with PlayerId - " + playerId);
            return;
        }

        playerPresentation.ShowCards(speedScaleCards, OnComplete);
    }

    public void HideCards(int playerId, Action OnComplete)
    {
        var playerPresentation = GetPlayerPresentation(playerId);

        if (playerPresentation == null)
        {
            Debug.LogWarning("Not found PlayerPresentation with PlayerId - " + playerId);
            return;
        }

        playerPresentation.HideCards(speedScaleCards, OnComplete);
    }

    private PlayerPresentation GetPlayerPresentation(int playerId)
    {
        return playerPresentations.Find(data => data.PlayerId == playerId);
    }
}

[System.Serializable]
public class PlayerPresentation
{
    public int PlayerId => playerId;

    [SerializeField] private int playerId;
    [SerializeField] private List<PlayerPresentationTransform> playerPresentationTransforms = new();

    [Header("Object")]
    [SerializeField] private Transform transformPlayerParent;
    [SerializeField] private Transform transformPlayerScale;

    [Header("Balance")]
    [SerializeField] private Transform transformBalance;

    [Header("Avatar")]
    [SerializeField] private Transform transformAvatar;
    [SerializeField] private Vector3 positionAvatarNormal;
    [SerializeField] private Vector3 positionAvatarWithBalance;

    [Header("Cards")]
    [SerializeField] private Transform transformCards;

    private Tween tweenScalePlayer;
    private Tween tweenMovePlayer;
    private Tween tweenMoveAvatar;
    private Tween tweenScaleBalance;
    private Tween tweenScaleCards;

    public void Show(float speedScalePlayer, Action OnComplete)
    {
        tweenScalePlayer?.Kill();

        transformPlayerScale.gameObject.SetActive(true);
        tweenScalePlayer = transformPlayerScale.DOScale(1, speedScalePlayer).OnComplete(() => OnComplete?.Invoke());
    }

    public void Hide(float speedScalePlayer, Action OnComplete)
    {
        tweenScalePlayer?.Kill();

        tweenScalePlayer = transformPlayerScale.DOScale(0, speedScalePlayer).OnComplete(() =>
        {
            transformPlayerScale.gameObject.SetActive(false);
            OnComplete?.Invoke();
        });
    }

    public void MoveToLayout(string key, float speedMovePlayer, Action OnComplete = null)
    {
        tweenMovePlayer?.Kill();

        var transformMove = GetTransform(key);

        if(transformMove == null)
        {
            Debug.LogWarning($"Not found TransformMove by PlayerId - {playerId} with key - {key}");
            return;
        }

        tweenMovePlayer = transformPlayerParent.DOLocalMove(transformMove.localPosition, speedMovePlayer).OnComplete(() => OnComplete?.Invoke());
    }

    public void ShowCards(float speedScaleCards, Action OnComplete)
    {
        tweenScaleCards?.Kill();

        tweenScaleCards = transformCards.DOScale(1, speedScaleCards).OnComplete(() => OnComplete?.Invoke());
    }

    public void HideCards(float speedScaleCards, Action OnComplete)
    {
        tweenScaleCards?.Kill();

        tweenScaleCards = transformCards.DOScale(0, speedScaleCards).OnComplete(() => OnComplete?.Invoke());
    }

    public void ShowBalance(float speedMoveAvatar, float speedScaleBalance, Action OnComplete)
    {
        tweenMoveAvatar?.Kill();
        tweenScaleBalance?.Kill();

        tweenMoveAvatar = transformAvatar.DOLocalMove(positionAvatarWithBalance, speedMoveAvatar).OnComplete(() => OnComplete?.Invoke());
        tweenScaleBalance = transformBalance.DOScale(1, speedScaleBalance);
    }

    public void HideBalance(float speedMoveAvatar, float speedScaleBalance, Action OnComplete)
    {
        tweenMoveAvatar?.Kill();
        tweenScaleBalance?.Kill();

        tweenMoveAvatar = transformAvatar.DOLocalMove(positionAvatarNormal, speedMoveAvatar).OnComplete(() => OnComplete?.Invoke());
        tweenScaleBalance = transformBalance.DOScale(0, speedScaleBalance);
    }

    private Transform GetTransform(string key)
    {
        return playerPresentationTransforms.Find(data => data.Key == key).TransformMove;
    }
}

[System.Serializable]
public class PlayerPresentationTransform
{
    [SerializeField] private string key;
    [SerializeField] private Transform transformMove;

    public string Key => key;
    public Transform TransformMove => transformMove;
}

